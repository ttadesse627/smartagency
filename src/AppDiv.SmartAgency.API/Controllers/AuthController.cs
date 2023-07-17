using MediatR;
using Microsoft.AspNetCore.Mvc;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Features.Auth.Login;
using AppDiv.SmartAgency.Application.Features.Auth.ForgotPassword;
using AppDiv.SmartAgency.Application.Features.Auth.ChangePassword;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        // [HttpPost("resetPassword")]
        // public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        // {
        //     return Ok(await Mediator.Send(command));
        // }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(await _mediator.Send(new LogoutCommand()));
        }
    }
}