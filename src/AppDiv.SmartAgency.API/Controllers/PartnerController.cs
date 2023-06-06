
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Create;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Delete;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Update;
using AppDiv.SmartAgency.Application.Features.Partners.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

    [ApiController]
    [Route("api/partner")]
    public class PartnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PartnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<PartnerResponseDTO>> CreatePartner(CreatePartnerCommand partnerRequest, CancellationToken token)
        {
            var response = await _mediator.Send(partnerRequest);
            return Ok(response);
        }

        [HttpGet("get-all-partner")]
        public async Task<ActionResult<GetAllPartnerResponseDTO>> GetAllPartners(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllPartnerQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PartnerResponseDTO> Get(Guid id,  [FromQuery]string fileType ,[FromQuery] string? folderType)
        {
            return await _mediator.Send(new GetPartnerByIdQuery(id, fileType, folderType));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeletePartner(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeletePartnerCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
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

                //var result = await _mediator.Send(command,id);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }


        }
    }

}