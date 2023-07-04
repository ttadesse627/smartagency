using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
using AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
using AppDiv.SmartAgency.Application.Features.Orders.Query;
using AppDiv.SmartAgency.Utility.Contracts;
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
    public async Task<ActionResult<ServiceResponse<String>>> Create(CreateOrderRequest request)
    {
        return Ok(await _mediator.Send(new CreateOrderCommand(request)));
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<ServiceResponse<GetOrderRespDTO>>> GetByIdAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetSingleOrder(id)));
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<GetOrdersResponseDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetAllOrders(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteOrderCommand(id)));
    }

    [HttpPut("edit")]
    public async Task<ActionResult> Edit(EditOrderRequest request)
    {
        return Ok(await _mediator.Send(new EditOrderCommand(request)));
    }

    [HttpGet("get-for-assignment")]
    public async Task<ActionResult<GetForAssignmentDTO>> GetOrderForAssignment()
    {
        return Ok(await _mediator.Send(new GetForAssignmentQuery()));
    }

    [HttpPut("assign")]
    public async Task<ActionResult> AssignOrder(OrderAssignmentRequest request)
    {
        return Ok(await _mediator.Send(new AssignOrderCommand(request)));
    }
    [HttpPut("unassign")]
    public async Task<ActionResult> UnassignOrder(UnassignOrderRequest request)
    {
        return Ok(await _mediator.Send(new UnassignOrderCommand(request)));
    }

    [HttpGet("show-order-status")]
    public async Task<ActionResult> ShowOrderStatus(Guid OrderId)
    {
        return Ok(await _mediator.Send(new ShowOrderStatusQuery(OrderId)));
    }

    [HttpGet("order-statuses")]
    public async Task<ActionResult> GetOrderStatuses()
    {
        return Ok(await _mediator.Send(new OrderStatusQuery()));
    }

    [HttpGet("get-for-enjaz")]
    public async Task<ActionResult<List<DropdownEnjazResponseDTO>>> GetOrderForEnjaz()
    {
        return Ok(await _mediator.Send(new GetForEnjazQuery()));
    }

}