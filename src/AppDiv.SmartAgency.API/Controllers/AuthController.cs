using Microsoft.AspNetCore.Mvc;
using AppDiv.SmartAgency.Application.Features.Auth.Login;
using AppDiv.SmartAgency.Application.Features.Auth.ForgotPassword;
using AppDiv.SmartAgency.Application.Features.Auth.ChangePassword;
using AppDiv.SmartAgency.Application.Features.Auth.ResetPassword;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(await Mediator.Send(new LogoutCommand()));
        }

    }
}