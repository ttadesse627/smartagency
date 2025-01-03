using MediatR;
using Microsoft.AspNetCore.Mvc;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Features.Auth.Login;
using AppDiv.SmartAgency.Application.Features.Auth.ForgotPassword;
using AppDiv.SmartAgency.Application.Features.Auth.ChangePassword;
using AppDiv.SmartAgency.Application.Features.Auth.ResetPassword;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private ISender _mediator = null!;

        protected new ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();


    }
}