
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.OnlineApplicants;
using AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Create;
using AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Delete;
using AppDiv.SmartAgency.Application.Features.OnlineApplicants.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

    [ApiController]
    [Route("api/onlineApplicant")]
    public class OnlineApplicantController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OnlineApplicantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<OnlineApplicantResponseDTO>> CreateOnlineApplicant(OnlineApplicantRequest onlineApplicantRequest, CancellationToken token)
        {
            var response = await _mediator.Send(new CreateOnlineApplicantCommand(onlineApplicantRequest));
            return Ok(response);
        }

        [HttpGet("get-all-online-applicant")]
        public async Task<ActionResult<OnlineApplicantResponseDTO>> GetAllOnlineApplicants(int pageNumber = 1, int pageSize = 15, string? searchTerm = null, string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllOnlineApplicantQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OnlineApplicantResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetOnlineApplicantByIdQuery(id));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteOnlineApplicant(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteOnlineApplicantCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}

