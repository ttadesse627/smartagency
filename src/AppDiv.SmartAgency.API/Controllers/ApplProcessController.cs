

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
    public async Task<ActionResult<ApplicantProcessResponseDTO>> CreateProcess(SubmitApplicantProcessRequest request)
    {
        var response = await _mediator.Send(new SubmitApplicantProcessCommand(request));
        return Ok(response.Data);
    }
    [HttpGet("get/{id}")]
    public async Task<ActionResult<ApplicantProcessResponseDTO>> GetApplicantProcessses([Required] Guid id, int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetApplProcessQuery(id, pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }


    // [HttpGet("get/{id}")]
    // public async Task<ActionResult<ServiceResponse<List<GetProcessDefinitionResponseDTO>>>> GetProcessDefinitions(Guid id)
    // {
    //     return Ok(await _mediator.Send(new GetProcessDefinitionsQuery(id)));
    // }

    // [HttpGet("get")]
    // public async Task<ActionResult<ServiceResponse<List<GetApplProcessResponseDTO>>>> GetApplicantProcessses(Guid id, int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    // {
    //     // return Ok(await _mediator.Send(new GetApplProcessQuery()));
    //     return Ok();
    // }

    // [HttpPut("edit")]
    // public async Task<ActionResult<ServiceResponse<Int32>>> EditProcess(EditProcessRequest request)
    // {
    //     var response = new ServiceResponse<Int32>();
    //     response = await _mediator.Send(new EditProcessCommand(request));
    //     return Ok(response);
    // }
}