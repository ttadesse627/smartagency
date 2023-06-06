
using System.Security.Claims;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Application.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(IList<string> roleNames)
        {
            var success = false;
            if (roleNames.Count > 0)
            {
                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName);

                    var result = await _roleManager.CreateAsync(role);
                    success = result.Succeeded;
                    if (!result.Succeeded)
                    {
                        throw new ValidationException(result.Errors);
                    }
                }
            }
            return success;
        }

        // Return multiple value
        public async Task<ServiceResponse<int>> CreateUserAsync(ApplicationUser user, string password)
        {
            var response = new ServiceResponse<int>();
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                response.Errors?.Add("user with the given email already exists");
            }
            existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser != null)
            {
                response.Errors?.Add("username is already taken");
            }
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                response.Errors = result.ToApplicationResult().Errors.ToList();
            }
            var userClaim = await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id));
            string userId = await _userManager.GetUserIdAsync(user);
            response.Message = $"Successfully created with new Id {userId}";
            return response;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId);
            if (roleDetails == null)
            {
                throw new NotFoundException("Role not found");
            }

            if (roleDetails.Name == "Administrator")
            {
                throw new BadRequestException("You can not delete Administrator Role");
            }
            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.FullName,
                x.UserName,
                x.Email
            }).ToListAsync();

            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email)).ToList();
        }

        public Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name
            }).ToListAsync();

            return roles.Select(role => (role.Id, role.Name)).ToList();
        }
        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
        }

        public async Task<ApplicationUser> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users
                    .Include(usr => usr.UserGroups)
                    .Include(usr => usr.Position)
                    .Include(usr => usr.Branch)
                    .Include(usr => usr.Partner)
                    .Include(usr => usr.Address)
                        .ThenInclude(addr => addr.Country)
                    .Include(usr => usr.Address)
                        .ThenInclude(addr => addr.AddressRegion)
                    .FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return user;
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return await _userManager.GetUserIdAsync(user);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return await _userManager.GetUserNameAsync(user);
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }

        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.FullName = fullName;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public async Task<bool> UpdateRole(string id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            result = await _userManager.AddToRolesAsync(user, usersRole);

            return result.Succeeded;
        }

        public async Task<(Result result, IList<string>? roles, string? userId)> AuthenticateUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);


            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return (Result.Success(), userRoles, user.Id);


            }
            string[] errors = new string[] { "Invalid login" };

            return (Result.Failure(errors), null, null);

        }

        public async Task<ServiceResponse<int>> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var response = new ServiceResponse<int>();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.Message = "could't find user with the given username";
                return response;
            }
            var changePResponse = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!changePResponse.Succeeded)
            {
                response.Errors.Add("Change password failed!");
                throw new Exception($"Change password failed! ");
            }
            return response;
        }

        public async Task<ServiceResponse<int>> UpdateUserAsync(string id, string username, string email, string fullName, string otpCode, DateTime expirySecond)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var response = new ServiceResponse<int>();

            if (user == null)
            {
                response.Success = false;
                response.Message = "could not find user with the given id";
                response.Errors?.Add("could not find user with the given id");
            }


            user.UserName = username;
            user.Email = email;
            user.Otp = otpCode;
            user.OtpExpiredDate = expirySecond;
            user.FullName = fullName;

            var updateResponse = await _userManager.UpdateAsync(user);

            if (!updateResponse.Succeeded)
            {
                response.Success = false;
                response.Errors.Add($"User Updating failed! \n {response.Errors}");
                throw new Exception($"User Updating failed! \n {response.Errors}");
            }

            return response;
        }
    }
}