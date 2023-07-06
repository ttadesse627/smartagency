

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
    [HttpGet("get/{processId}")]
    public async Task<ActionResult<JObject>> GetApplicantProcessses(Guid processId)
    {
        var response = new JObject();
        var resp = await _mediator.Send(new GetApplProcessQuery(processId));
        if (resp != null)
        {
            if (resp.ProcessReadyApplicants != null && resp.ProcessReadyApplicants.Count > 0)
            {
                var prReady = new JArray();

                prReady = (JArray)resp.ProcessReadyApplicants;
                response["ProcessReady"] = prReady;
            }

            var processDefs = new JArray();
            if (resp.ProcessDefinitions != null && resp.ProcessDefinitions.Count > 0)
            {
                processDefs = (JArray)resp.ProcessDefinitions;
                response["ProcessDefinitions"] = processDefs;
            }
        }
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
}