
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppDiv.CRVS.Application.Exceptions;
using AppDiv.CRVS.Application.Interfaces;
using AppDiv.CRVS.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AppDiv.CRVS.Application.Common;
using System.Text;

namespace AppDiv.CRVS.Application.Service
{
  public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IConfiguration _configuration;
        // private readonly TokenGeneratorService _tokenGeneratorService;

        public IdentityService(UserManager<ApplicationUser> userManager, ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _logger = logger;
            // _tokenGeneratorService = tokenGeneratorService;
        }
        public async Task<(Result result, IList<string>? roles, ApplicationUser? user)> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);


            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
               var userRoles = await _userManager.GetRolesAsync(user); 
                // var tokenString = _tokenGeneratorService.GenerateJWTToken((userId:user.Id , userName:user.UserName , roles:userRoles));
                return (Result.Success(), userRoles, user);


            }
            string[] errors = new string[] { "Invalid login" };
            return (Result.Failure(errors),null, null);

        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);


            return user.UserName;
        }
        public string GetUserGroupId(string userId){
            return  _userManager.Users.First(u => u.Id == userId).UserGroupId;
        }
        public async Task<(Result, string)> createUser(string userName, string email,  string personalInfoId, string userGroupId)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return (Result.Failure(new string[] { "user with the given email already exists" }), string.Empty);
            }
            existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return (Result.Failure(new string[] { "username is already taken" }), string.Empty);
            }
            var newUser = new ApplicationUser()
            {
                UserName = userName,
                Email = email,
                UserGroupId = userGroupId,
                PersonalInfoId = personalInfoId
            };
            string password = GeneratePassword();
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                return (result.ToApplicationResult(), string.Empty);
            }
            return (Result.Success(), password);
        }

        public async Task<(Result, string)> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (Result.Failure(new string[] { "could not find user with the given email" }), string.Empty);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return (Result.Success(), token);
        }
        public async Task<Result> ResetPassword(string email, string password, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure(new string[] { "user not found" });
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                throw new Exception($"password reset failed! \n {resetPassResult.Errors}");
            }
            return Result.Success();
        }

        public async Task<Result> ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given email" });
            }
            var response = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!response.Succeeded)
            {
                throw new Exception($"Change password failed! \n {response.Errors}");
            }
            return Result.Success();
        }

        public async Task<Result> UpdateUser(string id, string userName, string email,  string personalInfoId, string userGroupId){

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given id" });
            }

          
            user.UserName = userName;
            user.Email = email;
            user.UserGroupId = userGroupId;
            user.PersonalInfoId = personalInfoId;

            var response = await _userManager.UpdateAsync(user);

            if (!response.Succeeded)
            {
                throw new Exception($"User Updating failed! \n {response.Errors}");
            }

            return Result.Success();

        }
        public async Task<Result> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given id" });
            }
            var response = await _userManager.DeleteAsync(user);

            if (!response.Succeeded)
            {
                throw new Exception($"User Deleting failed! \n {response.Errors}");
            }

            return Result.Success();
        }
   
        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));


            return password.ToString();
        }
        public IQueryable<ApplicationUser> AllUsers()
        {
            return _userManager.Users;
        }

    }
}
