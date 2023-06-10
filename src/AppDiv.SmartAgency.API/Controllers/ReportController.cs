
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
    public async Task<ActionResult<ApplReportDTO>> GetApplicantReport(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending,
        [FromQuery] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new ApplicantReportQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection, filters)));
    }
    [HttpGet("not-assigned-applicant-report")]
    public async Task<ActionResult<ApplReportDTO>> GetNotAssignedApplicantReport(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending,
        [FromQuery] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new NotAssignedApplicantReportQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection, filters)));
    }

    [HttpGet("visa-report")]
    public async Task<ActionResult<ApplReportDTO>> GetVisaReport(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending,
        [FromQuery] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new NotAssignedApplicantReportQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection, filters)));
    }
}