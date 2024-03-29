﻿using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces
{
    public interface IIdentityService
    {
        // User section
        Task<ServiceResponse<int>> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> SigninUserAsync(string userName, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<ApplicationUser> GetByUsernameAsync(string userName);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        Task<ApplicationUser> GetUserDetailsByUserNameAsync(string userName);
        Task<string> GetUserNameAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> IsUniqueUserName(string userName);

        Task<Result> ResetPassword(string? email, string? userName, string password, string token);
        public string GeneratePassword();
        Task<Result> UpdateResetOtp(string id, string? otp, DateTime? otpExpiredDate);

        Task<(Result result, string resetToken)> ForgotPassword(string? email, string? userName);

        public Task<Result> VerifyOtp(string userName, string otp);
        public Task<(Result result, string? email, string? phone)> ReGenerateOtp(string userId, string otp, DateTime otpExpiry);

        Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync();
        Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync();
        Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);
        Task<ServiceResponse<int>> UpdateUserAsync(string id, string username, string email, string fullName, string otpCode, DateTime expirySecond);

        // Role Section
        Task<bool> CreateRoleAsync(IList<string> roleNames);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<bool> UpdateRole(string id, string roleName);

        // User's Role section
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<bool> AssignUserToRole(string userName, IList<string> roles);
        Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);
        Task<(Result result, IList<string>? roles, string? userId)> AuthenticateUser(string userName, string password);
        Task<ServiceResponse<int>> ChangePassword(string userName, string oldPassword, string newPassword);
    }
}

