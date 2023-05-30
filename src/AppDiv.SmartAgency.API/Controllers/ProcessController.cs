

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Create;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;
[ApiController]
[Route("api/process")]
public class ProcessController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProcessController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<Int32>>> CreateProcess(CreateProcessRequest request)
    {
        var response = new ServiceResponse<Int32>();
        response = await _mediator.Send(new CreateProcessCommand(request));
        return Ok(response);
    }
    [HttpGet("get")]
    public async Task<ActionResult<ServiceResponse<List<GetProcessResponseDTO>>>> GetProcesses()
    {
        return Ok(await _mediator.Send(new GetProcessQuery()));
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ServiceResponse<Int32>>> EditProcess(EditProcessRequest request)
    {
        var response = new ServiceResponse<Int32>();
        response = await _mediator.Send(new EditProcessCommand(request));
        return Ok(response);
    }
}