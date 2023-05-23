
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
using AppDiv.SmartAgency.Application.Features.Query.DeletedInfos;
using AppDiv.SmartAgency.Utility.Contracts;
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
    public async Task<ActionResult<GetOrdersResponseDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetDeletedOrders(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }
    
    [HttpPut("order/restore/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        return Ok(await _mediator.Send(new RestoreDeleteOrderCommand(id)));
    }

    [HttpPut("order/get/{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _mediator.Send(new RestoreDeleteOrderCommand(id)));
    }
}