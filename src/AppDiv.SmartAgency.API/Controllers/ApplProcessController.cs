

using System.ComponentModel.DataAnnotations;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Create;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;
[ApiController]
[Route("api/applicant-process")]
public class ApplProcessController : ControllerBase
{
    private readonly IMediator _mediator;
    public ApplProcessController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<ApplicantProcessResponseDTO>>> CreateProcess(SubmitApplicantProcessRequest request)
    {
        var response = await _mediator.Send(new SubmitApplicantProcessCommand(request));
        return Ok(response);
    }
    [HttpGet("get/{id}")]
    public async Task<ActionResult<List<GetProcessDefinitionResponseDTO>>> GetApplicantProcessses([Required] Guid id, int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        var response = await _mediator.Send(new GetApplProcessQuery(id, pageNumber, pageSize, searchTerm, orderBy, sortingDirection));
        return Ok(response);
    }
}