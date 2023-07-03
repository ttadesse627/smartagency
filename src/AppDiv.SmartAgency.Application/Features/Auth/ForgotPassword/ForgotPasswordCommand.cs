
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Utility.Config;
using AppDiv.SmartAgency.Utility.Exceptions;
using AppDiv.SmartAgency.Utility.Services;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace AppDiv.SmartAgency.Application.Features.Auth.ForgotPassword
{
    public record ForgotPasswordCommand : IRequest<object>
    {
        public string UserName { get; init; }
        public string ClientURI { get; init; }

    }
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, object>
    {
        private readonly IIdentityService _identityService;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IOptions<SMTPServerConfiguration> config;
        private readonly SMTPServerConfiguration _config;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(IIdentityService identityService, IMailService mailService, ISmsService smsService, IOptions<SMTPServerConfiguration> config, ILogger<ForgotPasswordCommandHandler> logger)
        {
            _identityService = identityService;
            _mailService = mailService;
            _smsService = smsService;
            this.config = config;
            _config = config.Value;
            _logger = logger;
        }
        public async Task<object> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {

                await sendOTP(request, cancellationToken);
                return new { message = "successfully sent password reset by email and phone" };


            }
            catch (Exception)
            {
                throw;
            }


        }
        // private async Task<bool> sendByEmailAsync(ForgotPasswordCommand request, CancellationToken cancellationToken)
        // {
        //     // var response = await _identityService.ForgotPassword(request.Email);
        //     // if (!response.result.Succeeded)
        //     // {
        //     //     throw new Exception(response.result.Errors.ToString());
        //     // }
        //     // var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(response.resetToken)); 
        //     // var param = new Dictionary<string, string?>
        //     // {
        //     //     { "token" , token },
        //     //     { "email" , request.Email }
        //     // };

        //     // var callback = QueryHelpers.AddQueryString(request.ClientURI, param);
        //     // var emailContent = "Please use the link below to reset your password\n" + callback;
        //     // var subject = "Reset Password";
        //     // await _mailService.SendAsync(body: emailContent, subject: subject, senderMailAddress: _config.SENDER_ADDRESS, receiver: request.Email, cancellationToken);
        //     // return  true;
        // }
        private async Task<bool> sendOTP(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _identityService.GetByUsernameAsync(request.UserName);
            if (user == null)
            {
                throw new NotFoundException("user not found");
            }
            int expirySecond = 120;
            //send sms and get otp code
            var otpCode = await _smsService.SendOtpAsync(user.PhoneNumber, "", "is your password reset code ", expirySecond, 6, 0);
            var updateResponse = await _identityService.UpdateUserAsync(user.Id, user.UserName, user.Email, user.FullName, otpCode.ToString(), DateTime.Now.AddSeconds(expirySecond));
            if (!updateResponse.Success)
            {
                throw new Exception(string.Join(",", updateResponse.Errors));
            }
            //send to email
            var param = new Dictionary<string, string?>
            {
                { "otp" , otpCode.ToString() },
                { "userName" , request.UserName }
            };

            var callback = QueryHelpers.AddQueryString(request.ClientURI, param);
            var emailContent = "Please use the link below to reset your password\n" + callback;
            var subject = "Reset Password";
            await _mailService.SendAsync(body: emailContent, subject: subject, senderMailAddress: _config.SENDER_ADDRESS, receiver: user.Email, cancellationToken);

            return true;

        }
    }
}
