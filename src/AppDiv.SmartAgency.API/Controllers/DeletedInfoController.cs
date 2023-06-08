
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Features.DeletedInfos.Command.Update;
using AppDiv.SmartAgency.Application.Features.DeletedInfos.Query;
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
    public async Task<ActionResult<GetOrdersResponseDTO>> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetDeletedOrders(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

    [HttpPut("order/restore/{id}")]
    public async Task<ActionResult> DeleteOrder(Guid id)
    {
        return Ok(await _mediator.Send(new RestoreDeleteOrderCommand(id)));
    }

    [HttpPut("order/get/{id}")]
    public async Task<ActionResult<GetOrderRespDTO>> GetOrderByIdAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetDeletedOrderQuery(id)));
    }
    [HttpGet("applicant/get-all")]
    public async Task<ActionResult<ApplicantsResponseDTO>> GetAllApplicantAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetDeletedApplicants(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

    [HttpPut("applicant/restore/{id}")]
    public async Task<ActionResult> DeleteApplicant(Guid id)
    {
        return Ok(await _mediator.Send(new RestoreDeleteApplicantCommand(id)));
    }

    [HttpPut("applicant/get/{id}")]
    public async Task<ActionResult<GetApplicantResponseDTO>> GetApplicantByIdAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetDeletedApplicantQuery(id)));
    }
}