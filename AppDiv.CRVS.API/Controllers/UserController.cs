using AppDiv.CRVS.Application.Contracts.DTOs;
using AppDiv.CRVS.Application.Features.User.Command.Create;
using AppDiv.CRVS.Application.Features.User.Query;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppDiv.CRVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(Roles = "Admin, Management")]
    public class UserController : ApiControllerBase
    {
        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            var result = await Mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
            return Ok(result);
        }

    }
}