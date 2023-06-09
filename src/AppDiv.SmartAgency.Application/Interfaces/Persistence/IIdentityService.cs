﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces
{
    public interface IIdentityService
    {
      Task<string> GetUserNameAsync(string userId);
        Task<(Result result, IList<string>? roles, ApplicationUser? user)> AuthenticateUser(string email, string password);
        Task<(Result result, string password)> createUser( string userName, string email, string personalInfoId, string userGroupId);
        Task<(Result result, string resetToken)> ForgotPassword(string email);
        Task<Result> ResetPassword(string email, string password, string token);
        Task<Result> ChangePassword(string email, string oldPassword, string newPassword);
        Task<Result> UpdateUser(Guid id, string userName, string email, string personalInfoId, string userGroupId);
        IQueryable<ApplicationUser> AllUsers();
        Task<Result> DeleteUser(string userId);
        string GetUserGroupId(string userId);
    }
}

