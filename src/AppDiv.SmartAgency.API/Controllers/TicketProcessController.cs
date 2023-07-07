
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
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
    public async Task<ActionResult<TicketProcessResponseDTO>> GetOnTicketProcessApplicants()
    {
        return await _mediator.Send(new GetTicketProcessApplicantsQuery());
    }

    [HttpPost("submit-ticket-ready")]
    public async Task<ActionResult<TicketProcessResponseDTO>> SubmitTicketReady(SubmitTicketReadyRequest request)
    {
        var response = await _mediator.Send(new SubmitTicketReadyCommand(request));
        return Ok(response);
    }

    [HttpPost("register-ticket")]
    public async Task<ActionResult<TicketProcessResponseDTO>> RegisterTicket(SubmitRegisteredTicketRequest request)
    {
        var response = await _mediator.Send(new SubmitTicketRegistrationCommand(request));
        return Ok(response);
    }

    [HttpPost("ticket-refund")]
    public async Task<ActionResult<TicketProcessResponseDTO>> RefundTicket(SubmitTicketRefundRequest request)
    {
        var response = await _mediator.Send(new SubmitTicketRefundCommand(request));
        return Ok(response);
    }

    [HttpPost("rebook-ticket")]
    public async Task<ActionResult<TicketProcessResponseDTO>> RebookTicket(SubmitTicketRebookRequest request)
    {
        var response = await _mediator.Send(new SubmitTicketRebookCommand(request));
        return Ok(response);
    }

    [HttpPost("rebook-ticket-registration")]
    public async Task<ActionResult<TicketProcessResponseDTO>> RegisterRebookTicket(SubmitRegisteredTicketRequest request)
    {
        var response = await _mediator.Send(new SubmitTicketRebookRegCommand(request));
        return Ok(response);
    }

    [HttpPost("travel")]
    public async Task<ActionResult<TicketProcessResponseDTO>> RegisterTraveled(SubmitTraveledApplRequest request)
    {
        var response = await _mediator.Send(new SubmitTraveledApplCommand(request));
        return Ok(response);
    }
}