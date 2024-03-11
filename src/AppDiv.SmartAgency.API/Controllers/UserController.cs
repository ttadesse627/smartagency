using AppDiv.SmartAgency.Application.Features.Users.Command.Delete;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Features.Lookups.Query.GetAllUser;
using AppDiv.SmartAgency.Application.Features.User.Command.Update;
using AppDiv.SmartAgency.Application.Features.User.Query.GetUserById;
using AppDiv.SmartAgency.Application.Features.Users.Command.Create;
using AppDiv.SmartAgency.Application.Features.Users.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class UserController : ApiControllerBase
    {

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser(AddUserRequest request)
        {
            if (request.ConfirmationPassword != request.Password)
            {
                return BadRequest("The password doesn't match. Please try again!");
            }
            return Ok(await Mediator.Send(new CreateUserCommand(request)));
        }

        [HttpGet("user-details/{username}")]
        public async Task<IActionResult> GetUserDetailsByUserName(string username)
        {
            var result = await Mediator.Send(new GetUserDetailsByUserNameQuery(username));
            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<SearchModel<UserResponseDTO>> Get(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return await Mediator.Send(new GetAllUserQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection));
        }

        [HttpGet("get/{id}")]
        public async Task<UserDetailsResponseDTO> Get(Guid id)
        {
            return await Mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                if (request.Id == id)
                {
                    var result = await Mediator.Send(new UpdateUserCommand(request));
                    if (result.Success)
                    {
                        return Ok(result);
                    }
                    else return BadRequest(result);
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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteUser(Guid id)
        {
            try
            {
                var result = new ServiceResponse<int>();
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