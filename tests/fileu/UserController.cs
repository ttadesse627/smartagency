using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Features.Lookups.Query.GetAllUser;
using AppDiv.SmartAgency.Application.Features.User.Command.Create;
using AppDiv.SmartAgency.Application.Features.User.Command.Delete;
using AppDiv.SmartAgency.Application.Features.User.Command.Update;
using AppDiv.SmartAgency.Application.Features.User.Query;
using AppDiv.SmartAgency.Application.Features.User.Query.GetUserById;
using AppDiv.SmartAgency.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class UserController : ApiControllerBase
    {

        [HttpPost("Create")]
        // [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            var result = await Mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PaginatedList<UserResponseDTO>> Get([FromQuery] GetAllUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<FetchSingleUserResponseDTO> Get(string id)
        {
            return await Mediator.Send(new GetUserByIdQuery(id));
        }


        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(string id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await Mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                string result = string.Empty;
                result = await Mediator.Send(new DeleteUserCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

    }
}