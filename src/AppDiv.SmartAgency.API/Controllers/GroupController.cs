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
using AppDiv.SmartAgency.Application.Features.Groups.Commands.Update;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Features.Groups.Commands.Delete;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class GroupController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<GroupController> _Ilog;
        public GroupController(ISender mediator, ILogger<GroupController> Ilog)
        {
            _mediator = mediator;
            _Ilog = Ilog; ;
        }

        [HttpPost("create")]
        public async Task<ActionResult<GroupDTO>> CreateGroup([FromBody] CreateGroupCommand request)
        {
            var result = await _mediator.Send(request);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("get-all")]
        public async Task<SearchModel<FetchGroupDTO>> Get(int pageNumber = 1, int pageSize = 10, string searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return await _mediator.Send(new GetAllGroupQuery(pageNumber, pageSize, searchTerm!, orderBy!, sortingDirection));
        }
        [HttpGet("lookup")]
        public async Task<UserGroupResponseDTO> GetDropdown()
        {
            return await _mediator.Send(new GetDropDownGroups());
        }



        [HttpGet("get/{id}")]
        public async Task<GroupDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetGroupbyId(id));
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] UpdateGroupRequest request)
        {
            try
            {
                if (request.Id == id)
                {
                    var result = await _mediator.Send(new GroupUpdateCommand(request));
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
        [HttpDelete("delete/{id}")]
        public async Task<ServiceResponse<int>> DeleteLookup(Guid id)
        {
            return await _mediator.Send(new DeleteGroupCommand { Id = id });
        }
    }
}