
using AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
using AppDiv.SmartAgency.Application.Features.Reports;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;
    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("applicant-report")]
    public async Task<ActionResult<(SearchModel<ApplicantReportResponseDTO>, List<string>)>> GetApplicantReport(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending,
        [FromQuery] List<(string propertyName, string methodName, Object value)>? filters = null)
    {
        return Ok(await _mediator.Send(new ApplicantReportQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection, filters)));
    }

    // [HttpGet("get/{id}")]
    // public async Task<ActionResult<AttachmentResponseDTO>> GetByIdAsync(Guid id)
    // {
    //     return Ok(await _mediator.Send(new GetAttachmentQuery(id)));
    // }

    // [HttpDelete("delete/{id}")]
    // public async Task<ActionResult<ServiceResponse<int>>> DeleteAttachment(Guid id)
    // {
    //     var result = new ServiceResponse<int>();
    //     try
    //     {
    //         result = await _mediator.Send(new DeleteAttachmentCommand(id));
    //         result.Message = $"The attachment with id {id} is successfully deleted!";
    //         result.Success = true;
    //     }

    //     catch (System.Exception ex)
    //     {
    //         result.Message = ex.Message;
    //         result.Success = false;
    //     }

    //     if (result.Success)
    //     {
    //         return Ok(result);
    //     }
    //     else return BadRequest(result);
    // }

    // [HttpPut("edit/{id}")]
    // public async Task<ActionResult<ServiceResponse<int>>> EditAttachment(Guid id, [FromBody] EditAttachmentCommand request)
    // {
    //     var result = new ServiceResponse<int>();
    //     try
    //     {
    //         if (request.Id == id)
    //         {
    //             result = await _mediator.Send(request);
    //             return Ok(result);
    //         }
    //         else
    //         {
    //             return BadRequest();
    //         }
    //     }
    //     catch (Exception exp)
    //     {
    //         return BadRequest(exp.Message);
    //     }
    // }
}