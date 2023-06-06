
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Auth.ChangePassword
{
    public record ChangePasswordCommand : IRequest<ServiceResponse<int>>
    {
        public string UserName { get; init; }
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }

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
                throw new BadRequestException($"could not change password {string.Join(",", response.Errors)}");
            }
            response.Message = "password changed successfully";
            return response;
        }
    }
}
