
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Create;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;
[ApiController]
[Route("api/ticket-process")]
public class TicketProcessController : ControllerBase
{
    private readonly IMediator _mediator;
    public TicketProcessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-ticket-process")]
    public async Task<ActionResult<TicketProcessResponseDTO>> GetOnTicketProcessApplicants(Guid processId)
    {
        return await _mediator.Send(new GetTicketProcessApplicantsQuery(processId));
    }



    [HttpPost("submit-ticket-ready")]
    public async Task<ActionResult<TicketProcessResponseDTO>> CreateProcess(SubmitTicketReadyRequest request)
    {
        var response = await _mediator.Send(new SubmitTicketReadyCommand(request));
        return Ok(response);
    }
}