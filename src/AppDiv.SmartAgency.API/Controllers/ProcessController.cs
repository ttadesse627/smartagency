

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
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
    public async Task<ActionResult<ResponseContainerDTO<List<GetProcessResponseDTO>>>> GetProcesses()
    {
        return Ok(await _mediator.Send(new GetProcessQuery()));
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<ResponseContainerDTO<List<GetProcessDefinitionResponseDTO>>>> GetProcessDefinitions(Guid id)
    {
        return Ok(await _mediator.Send(new GetProcessDefinitionsQuery(id)));
    }

    [HttpPut("edit/{id}")]
    public async Task<ActionResult<ServiceResponse<Int32>>> EditProcess(Guid id, EditProcessRequest request)
    {
        var response = new ServiceResponse<Int32>();
        if (request.Id != id)
        {
            response.Message = $"The query id {id} and the body id {request.Id} must be the same.";
            return BadRequest(response);
        }
        response = await _mediator.Send(new EditProcessCommand(request));
        return Ok(response);
    }
}