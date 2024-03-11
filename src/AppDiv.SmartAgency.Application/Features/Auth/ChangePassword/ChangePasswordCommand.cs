
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Auth.ChangePassword
{
    public record ChangePasswordCommand : IRequest<ServiceResponse<int>>
    {
        public string UserName { get; init; } = string.Empty;
        public string OldPassword { get; init; } = string.Empty;
        public string NewPassword { get; init; } = string.Empty;

    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ServiceResponse<int>>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ServiceResponse<int>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.ChangePassword(request.UserName, request.OldPassword, request.NewPassword);
            if (!response.Success)
            {
                //throw new BadRequestException($"could not change password {string.Join(",", response.Errors)}");
                response.Message = "Coudn't Change Password";
                response.Success = false;
                return response;

            }
            response.Message = "password changed successfully";
            return response;
        }
    }
}
