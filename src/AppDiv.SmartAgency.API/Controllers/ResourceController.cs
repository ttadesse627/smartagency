using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;
using AppDiv.SmartAgency.Application.Features.Resources.Command.Create;
using AppDiv.SmartAgency.Application.Features.Resources.Command.Delete;
using AppDiv.SmartAgency.Application.Features.Resources.Command.Update;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Infrastructure.Authentication;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class ResourceController(IMediator mediator) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // [HasPermission("Resources",PermissionEnum.Write)]
        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateResource(string resourceName, CancellationToken token)
        {
            var response = await _mediator.Send(new CreateResourceCommand(resourceName), token);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HasPermission("Resources",PermissionEnum.Read)]
        [HttpGet("get-all")]
        public async Task<ActionResult<ResourceResponseDTO>> GetAllResources(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllResourcesQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("get/{id}")]
        public async Task<ResourceResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetResourceByIdQuery(id));
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteResource(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteResourceCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] UpdateResourceCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await _mediator.Send(command);
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
    }

}