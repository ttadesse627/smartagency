
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Application.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelperService _helperService;


        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, HelperService helperService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _helperService = helperService;
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
            else
            {
                var userClaim = await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id));
                string userId = await _userManager.GetUserIdAsync(user);
                response.Success = true;
                response.Message = $"Successfully created with new Id {userId}";
            }
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
                        .ThenInclude(addr => addr.Region)
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
            response.Errors = new List<string>();

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.Message = "Couldn't find user with the given username";
                return response;
            }


            var changePResponse = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            Console.WriteLine(changePResponse.Succeeded);
            if (!changePResponse.Succeeded)
            {
                response.Errors.Add("Change password failed!");
                // response.Message= changePResponse.Errors[0].Code;
                // throw new Exception($"Change password failed! ");
                response.Success = false;
                return response;
            }
            response.Success = true;
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
                return response;
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
                response.Errors?.Add($"User Updating failed! \n {response.Errors}");
                throw new Exception($"User Updating failed! \n {response.Errors}");
            }
            response.Success = true;
            return response;
        }


        public string GeneratePassword()
        {
            var policySetting = _helperService.getPasswordPolicySetting();
            var options = _userManager.Options.Password;
            int max = 0;
            bool digit;
            bool nonAlphanumeric = false;
            bool lowerCase = false;
            bool upperCase = false;
            if (policySetting != null)
            {
                max = policySetting.Max;
                digit = policySetting.Number;
                nonAlphanumeric = !(policySetting.Number && (policySetting.LowerCase || policySetting.UpperCase || policySetting.OtherChar));
                lowerCase = policySetting.LowerCase;
                upperCase = policySetting.UpperCase;
            }
            else
            {
                max = options.RequiredLength;
                nonAlphanumeric = options.RequireNonAlphanumeric;
                digit = options.RequireDigit;
                lowerCase = options.RequireLowercase;
                upperCase = options.RequireUppercase;

            }

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < max)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowerCase = false;
                else if (char.IsUpper(c))
                    upperCase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowerCase)
                password.Append((char)random.Next(97, 123));
            if (upperCase)
                password.Append((char)random.Next(65, 91));


            return password.ToString();
        }


        public async Task<Result> UpdateResetOtp(string id, string? otp, DateTime? otpExpiredDate)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given id" });
            }
            user.PasswordResetOtp = otp;
            user.PasswordResetOtpExpiredDate = otpExpiredDate;
            var response = await _userManager.UpdateAsync(user);

            if (!response.Succeeded)
            {
                throw new NotFoundException($"User Updating failed! \n {response.Errors}");
            }

            return Result.Success();

        }

        public async Task<Result> ResetPassword(string? email, string? userName, string password, string token)
        {
            var user = email != null
                        ? await _userManager.FindByEmailAsync(email)
                        : await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Result.Failure(new string[] { "user not found" });
            }
            var isLoginOtp = user.Otp != null;
            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                throw new NotFoundException($"password reset failed! \n {string.Join(",", errors)}\n {token}");
            }
            if (isLoginOtp)//login otp
            {
                user.Otp = null;
                user.OtpExpiredDate = DateTime.Now.AddDays(_helperService.getOtpExpiryDurationSetting());
            }
            else
            {
                user.PasswordResetOtp = null;
                user.PasswordResetOtpExpiredDate = null;
            }
            await _userManager.UpdateAsync(user);
            return Result.Success();
        }


        public async Task<(Result, string)> ForgotPassword(string? email, string? userName)
        {
            var user = email != null
                            ? await _userManager.FindByEmailAsync(email)
                            : await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return (Result.Failure(new string[] { "could not find user with the given email" }), string.Empty);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return (Result.Success(), token);
        }


        public async Task<Result> VerifyOtp(string userName, string otp)
        {
            var user = await _userManager.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException($"user with username {userName} is not found");
            }
            if (user.Otp != otp)
            {
                throw new AuthenticationException("invalid otp");
            }
            user.Otp = null;
            user.OtpExpiredDate = DateTime.Now.AddDays(_helperService.getOtpExpiryDurationSetting());
            await _userManager.UpdateAsync(user);

            return Result.Success();
        }

        public Task<(Result result, string? email, string? phone)> ReGenerateOtp(string userId, string otp, DateTime otpExpiry)
        {
            throw new NotImplementedException();
        }
    }
}