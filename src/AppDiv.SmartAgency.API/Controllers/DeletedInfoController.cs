
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Features.Query.DeletedInfos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;

[ApiController]
[Route("api/deleted-info")]
public class DeletedInfoController : ControllerBase
{
    private readonly IMediator _mediator;
    public DeletedInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("order/get-all")]
    public async Task<ActionResult<OrderResponseDTO>> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetDeletedOrders()));
    }
}