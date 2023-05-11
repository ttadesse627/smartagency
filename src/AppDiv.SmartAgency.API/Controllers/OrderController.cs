using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Update.Orders;
using AppDiv.SmartAgency.Application.Features.Query.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<String>>> Create(CreateOrderCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<OrderResponseDTO>> GetByIdAsync([FromQuery] Guid id)
    {
        return Ok(await _mediator.Send(new GetSingleOrder(id)));
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<OrderResponseDTO>> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllOrders()));
    }

    [HttpPut("delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteOrderCommand(id)));
    }

    [HttpPut("edit")]
    public async Task<ActionResult> Edit(EditOrderCommand request)
    {
        return Ok(await _mediator.Send(request));
    }


}