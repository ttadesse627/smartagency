using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Features.Command.Create.ApplicantsFollowupStatus;
using AppDiv.SmartAgency.Application.Features.Query.ApplicantFollowupStatuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/applicantp-followup-status")]
    public class ApplicantFollowupStatusController: ControllerBase
{
    private readonly IMediator _mediator;
    public ApplicantFollowupStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApplicantFollowupStatusResponseDTO>> CreateDeposit(CreateApplicantFollowupStatusCommand applicantFollowupStatusRequest, CancellationToken token)
    {
        var response = await _mediator.Send(applicantFollowupStatusRequest);
        return Ok(response);
    }

   

   [HttpGet("get-all-applicantFollowupStatuses")]
    public async Task<ActionResult<ApplicantFollowupStatusResponseDTO>> GetAllApplicantFollowupStatuses()
    {
        return Ok(await _mediator.Send(new GetAllApplicantFollowupStatusQuery()));
    }
 
  
     [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<CreateApplicantFollowupStatusRequest> Get(Guid id)
        {
            return await _mediator.Send(new GetApplicantFollowupStatusByIdQuery(id));
        }
  /*

      [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteDeposit(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteDepositCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }



     
        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditDepositCommand command)
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
          
  */
}
}

