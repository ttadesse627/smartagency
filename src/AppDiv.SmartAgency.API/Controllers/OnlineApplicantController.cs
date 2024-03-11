
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
    public class OnlineApplicantController(IMediator mediator) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        public async Task<ActionResult<OnlineApplicantResponseDTO>> CreateOnlineApplicant(OnlineApplicantRequest onlineApplicantRequest, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateOnlineApplicantCommand(onlineApplicantRequest), cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-all-online-applicant")]
        public async Task<ActionResult<OnlineApplicantResponseDTO>> GetAllOnlineApplicants(int pageNumber = 1, int pageSize = 15, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllOnlineApplicantQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("{id}")]
        public async Task<OnlineApplicantResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetOnlineApplicantByIdQuery(id));
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteOnlineApplicants([FromQuery] List<Guid> ids)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteOnlineApplicantCommand(ids));
                return Ok(result);

            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

    }
}

