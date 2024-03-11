using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Create;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Update;
using AppDiv.SmartAgency.Application.Features.Partners.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class PartnerController(IMediator mediator) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        public async Task<ActionResult<PartnerResponseDTO>> CreatePartner(CreatePartnerRequest partnerRequest)
        {
            var response = await _mediator.Send(new CreatePartnerCommand(partnerRequest));
            return Ok(response);
        }

        [HttpGet("get-all-partner")]
        public async Task<ActionResult<GetAllPartnerResponseDTO>> GetAllPartners(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllPartnerQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("{id}")]
        public async Task<PartnerResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetPartnerByIdQuery(id));
        }

        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditPartnerCommand command)
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

        [HttpGet("get-partner-dropdown")]
        public async Task<ActionResult<PartnerDropdownContainerDTO>> GetPartnerDropdown()
        {
            return Ok(await _mediator.Send(new GetPartnerDropdownQuery()));
        }
    }

}