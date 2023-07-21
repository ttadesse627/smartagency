
// using System.Text;
// using AppDiv.CRVS.Application.Exceptions;
// using AppDiv.CRVS.Application.Interfaces;
// using AppDiv.CRVS.Domain.Repositories;
// using MediatR;
// using Microsoft.AspNetCore.WebUtilities;

using System.Security.Authentication;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Auth.ResetPassword
{

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, object>
    {
        private readonly IIdentityService _identityService;
        public ResetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<object> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetByUsernameAsync(request.resetPassword.UserName);
            if (user == null)
            {
                throw new NotFoundException("user not found");
            }
            if (user.OtpExpiredDate == null || DateTime.Compare((DateTime)user.OtpExpiredDate, DateTime.Now) < 0 || user.Otp != request.resetPassword.Otp)
            {
                throw new AuthenticationException("could not authenticate user");
            }
            var forgotPasswordRes = await _identityService.ForgotPassword(email: null, user.UserName);
            if (!forgotPasswordRes.result.Succeeded)
            {
                throw new Exception(forgotPasswordRes.result.Errors.ToString());
            }
            var resetPasswordRes = await _identityService.ResetPassword(email: null, user.UserName, request.resetPassword.Password, forgotPasswordRes.resetToken);
            if (!resetPasswordRes.Succeeded)
            {
                throw new Exception();
            }
            return new { message = "password reset successful" };

        }
    }
}
