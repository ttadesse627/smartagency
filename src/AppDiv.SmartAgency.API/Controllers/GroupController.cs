
using MediatR;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroups;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroup;
using AppDiv.SmartAgency.Application.Features.Groups.Commands.Create;
using AppDiv.SmartAgency.Application.Contracts.Request.Groups;
using AppDiv.SmartAgency.Application.Features.Groups.Query.GetGroupById;

namespace AppDiv.SmartAgency.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/group")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Member,User")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<GroupController> _Ilog;
        public GroupController(ISender mediator, ILogger<GroupController> Ilog)
        {
            _mediator = mediator;
            _Ilog = Ilog; ;
        }


        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<SearchModel<FetchGroupDTO>> Get([FromQuery] GetAllGroupQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("lookup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<DropDownDto>> GetDropdown()
        {
            return await _mediator.Send(new GetDropDownGroups());
        }

        [HttpPost("create")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<GroupDTO>> CreateGroup([FromBody] AddGroupRequest request)
        {
            var result = await _mediator.Send(new CreateGroupCommand(request));
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<GroupDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetGroupbyId(id));
        }

        // [HttpPut("Edit/{id}")]
        // public async Task<ActionResult> Edit(Guid id, [FromBody] GroupUpdateCommand command)
        // {
        //     try
        //     {
        //         if (command.group.Id == id)
        //         {
        //             var result = await _mediator.Send(command);
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return BadRequest();
        //         }
        //     }
        //     catch (Exception exp)
        //     {
        //         return BadRequest(exp.Message);
        //     }
        // }


        // [HttpDelete("Delete/{id}")]
        // public async Task<BaseResponse> DeleteLookup(Guid id)
        // {
        //     try
        //     {
        //         string result = string.Empty;
        //         return await _mediator.Send(new DeleteGroupCommands { Id = id });
        //     }
        //     catch (Exception exp)
        //     {
        //         var res = new BaseResponse
        //         {
        //             Success = false,
        //             Message = exp.Message
        //         };
        //         return res;
        //     }
        // }




    }
}