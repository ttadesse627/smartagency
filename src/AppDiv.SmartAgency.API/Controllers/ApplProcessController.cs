using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Update;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;
public class ApplicantProcessController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get/{processId}")]
    public async Task<ActionResult<ApplicantProcessResponseDTO>> GetApplicantProcessses(Guid processId)
    {
        var response = await _mediator.Send(new GetApplProcessQuery(processId));

        return Ok(response);
    }
    [HttpPost("submit-to-process")]
    public async Task<ActionResult<List<GetProcessDefinitionResponseDTO>>> CreateProcess(SubmitApplicantProcessRequest request)
    {
        var response = await _mediator.Send(new SubmitApplicantProcessCommand(request));
        return Ok(response);
    }
    [HttpPost("step-back")]
    public async Task<ActionResult<ServiceResponse<ApplicantProcessResponseDTO>>> StepBack(StepbackProcessRequest request)
    {
        var response = await _mediator.Send(new StepbackProcessCommand(request));
        return Ok(response);
    }

    [HttpPost("edit-statuses")]
    public async Task<ActionResult> EditOrderStatuses(EditOrderStatusRequest StatusRequest)
    {
        return Ok(await _mediator.Send(new EditOrderStatusCommand(StatusRequest)));
    }
}