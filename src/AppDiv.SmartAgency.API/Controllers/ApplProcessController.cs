

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;
[ApiController]
[Route("api/applicant-process")]
public class ApplicantProcessController : ControllerBase
{
    private readonly IMediator _mediator;
    public ApplicantProcessController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("submit-to-process")]
    public async Task<ActionResult<List<GetProcessDefinitionResponseDTO>>> CreateProcess(SubmitApplicantProcessRequest request)
    {
        var response = await _mediator.Send(new SubmitApplicantProcessCommand(request));
        return Ok(response);
    }
    [HttpGet("get/{processId}")]
    public async Task<ActionResult<List<GetProcessDefinitionResponseDTO>>> GetApplicantProcessses(Guid processId)
    {
        var response = await _mediator.Send(new GetApplProcessQuery(processId));
        return Ok(response);
    }

    [HttpPost("step-back")]
    public async Task<ActionResult<ServiceResponse<ApplicantProcessResponseDTO>>> StepBack(StepbackProcessRequest request)
    {
        var response = await _mediator.Send(new StepbackProcessCommand(request));
        return Ok(response);
    }
}