using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ApplicantFollowupStatuses;
using AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Create;
using AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Delete;
using AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Update;
using AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Query;

using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/applicantp-followup-status")]
    public class ApplicantFollowupStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicantFollowupStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ApplicantFollowupStatusResponseDTO>> CreateApplicantFollowup(CreateApplicantFollowupStatusRequest applicantFollowupStatusRequest, CancellationToken token)
        {
            var response = await _mediator.Send(new CreateApplicantFollowupStatusCommand(applicantFollowupStatusRequest));
            return Ok(response);
        }



        [HttpGet("get-all-applicantFollowupStatuses")]
        public async Task<ActionResult<ApplicantFollowupStatusResponseDTO>> GetAllApplicantFollowupStatuses(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllApplicantFollowupStatusQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<GetApplicantFollowupStatusByIdResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetApplicantFollowupStatusByIdQuery(id));
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteFollowupStatuses([FromQuery]List<Guid> ids)
        { 
            try
            {
                string result= string.Empty;
                result= await _mediator.Send(new DeleteApplicantFollowupStatusCommand(ids));
                return Ok(result);
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message); 
            }
            
        } 

        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditApplicantFollowupStatusCommand command)
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
