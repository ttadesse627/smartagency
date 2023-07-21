
using AppDiv.SmartAgency.Application.Contracts.Request.Auth;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Auth.ResetPassword
{
    // Customer create command with CustomerResponse

    public record ResetPasswordCommand(ResetPasswordRequest resetPassword) : IRequest<object>
    {

    }
}