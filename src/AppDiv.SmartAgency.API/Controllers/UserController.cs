// using AppDiv.SmartAgency.Application.Common;
// using AppDiv.SmartAgency.Application.Contracts.DTOs;
// using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
// using AppDiv.SmartAgency.Application.Features.Users.Command.Create;
// using AppDiv.SmartAgency.Application.Features.Users.Query;
// using AppDiv.SmartAgency.Domain;
// using MediatR;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class UserController : ApiControllerBase
    {

        // [HttpPost("create")]
        // // [ProducesDefaultResponseType(typeof(int))]
        // public async Task<ActionResult> CreateUser(AddUserRequest request)
        // {
        //     if (request.ConfirmationPassword != request.Password)
        //     {
        //         return BadRequest("The password is not confirmed. Please try again!");
        //     }
        //     return Ok(await Mediator.Send(new CreateUserCommand(request)));
        // }


        //         [HttpGet("get-user-details-by-username/{userName}")]
        //         [ProducesDefaultResponseType(typeof(UserResponseDTO))]
        //         public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        //         {
        //             var result = await Mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
        //             return Ok(result);
        //         }

        //         [HttpGet("get-all")]
        //         [ProducesResponseType(StatusCodes.Status200OK)]
        //         public async Task<UserResponseDTO> Get([FromQuery] GetAllUserQuery query)
        //         {
        //             return await Mediator.Send(query);
        //         }

        //         [HttpGet("get/{id}")]
        //         [ProducesResponseType(StatusCodes.Status200OK)]
        //         public async Task<UserDetailsResponseDTO> Get(string id)
        //         {
        //             return await Mediator.Send(new GetUserByIdQuery(id));
        //         }


        //         [HttpPut("edit/{id}")]
        //         public async Task<ActionResult> Edit(string id, [FromBody] UpdateUserCommand command)
        //         {
        //             try
        //             {
        //                 if (command.Id == id)
        //                 {
        //                     var result = await Mediator.Send(command);
        //                     return Ok(result);
        //                 }
        //                 else
        //                 {
        //                     return BadRequest();
        //                 }
        //             }
        //             catch (Exception exp)
        //             {
        //                 return BadRequest(exp.Message);
        //             }
        //         }

        //         [HttpDelete("delete/{id}")]
        //         public async Task<ActionResult> DeleteUser(string id)
        //         {
        //             try
        //             {
        //                 string result = string.Empty;
        //                 result = await Mediator.Send(new DeleteUserCommand(id));
        //                 return Ok(result);
        //             }
        //             catch (Exception exp)
        //             {
        //                 return BadRequest(exp.Message);
        //             }
        //         }

    }
}