
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

    [HttpPost("applicant-report")]
    public async Task<ActionResult<ApplReportDTO>> GetApplicantReport([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromBody] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new ApplicantReportQuery(pageNumber, pageSize, filters)));
    }
    [HttpPost("not-assigned-applicant-report")]
    public async Task<ActionResult<ApplReportDTO>> GetNotAssignedApplicantReport([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromBody] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new NotAssignedApplicantReportQuery(pageNumber, pageSize, filters)));
    }

    [HttpPost("unassigned-visa-report")]
    public async Task<ActionResult<ApplReportDTO>> GetVisaReport([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromBody] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new NotAssignedApplicantReportQuery(pageNumber, pageSize, filters)));
    }
}