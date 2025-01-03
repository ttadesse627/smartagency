using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.LookUps;
using AppDiv.SmartAgency.Application.Features.LookUps.Command.Create;
using AppDiv.SmartAgency.Application.Features.LookUps.Command.Delete;
using AppDiv.SmartAgency.Application.Features.LookUps.Command.Update;
using AppDiv.SmartAgency.Application.Features.LookUps.Query;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Infrastructure.Authentication;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class LookUpController(IMediator mediator) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HasPermission("Lookups",PermissionEnum.WriteMember)]
        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateLookUp(CreateLookUpRequest lookUpRequest, CancellationToken token)
        {
            var response = await _mediator.Send(new CreateLookUpCommand(lookUpRequest), token);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HasPermission("Lookups",PermissionEnum.ReadMember)]
        [HttpGet("get-all-lookup")]
        public async Task<ActionResult<LookUpResponseDTO>> GetAllLookUps(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllLookUps(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("get/{id}")]
        public async Task<LookUpResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetLookUpByIdQuery(id));
        }

        [HttpGet("get/categories")]
        public async Task<Dictionary<string, List<LookUpItemResponseDTO>>> Get([FromQuery] List<string> category)
        {
            var items = await _mediator.Send(new GetLookUpByCategoryQuery(category));
            return items;
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteLookUp(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteLookUpCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditLookUpCommand command)
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