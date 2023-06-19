
using AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
using AppDiv.SmartAgency.Application.Features.Reports;
using AppDiv.SmartAgency.Application.Features.Reports.Query;
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
    // public async Task<ActionResult<ApplReportDTO>> GetApplicantReport(int pageNumber = 1, int pageSize = 10, List<FilterPropsRequest>? filters = null)
    // {
    //     return Ok(await _mediator.Send(new ApplicantReportQuery(pageNumber, pageSize, filters)));
    // }
    public async Task<ActionResult<ApplicantReportResponseDTO>> GetApplicantReport(int pageNumber=1, int pageSize= 10, string? searchTerm= "")
    {

       return Ok(await _mediator.Send(new GetApplicantReportQuery(pageNumber, pageSize, searchTerm)));
    }

    /*[HttpGet("not-assigned-applicant-report")]
    public async Task<ActionResult<ApplReportDTO>> GetNotAssignedApplicantReport(int pageNumber = 1, int pageSize = 10, [FromQuery] List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new NotAssignedApplicantReportQuery(pageNumber, pageSize, filters)));
    }
    */

  [HttpPost("not-assigned-applicant-report")]
  public async Task<ActionResult<ApplicantReportResponseDTO>> GetNotAssignedApplicant(int pageNumber=1, int pagSize=10 , string? searchTerm="")
  {
        return Ok(await _mediator.Send(new GetNotAssignedApplicantReportQuery(pageNumber, pagSize, searchTerm)));
  }


    [HttpPost("unassigned-visa-report")]
    public async Task<ActionResult<ApplReportDTO>> GetVisaReport(int pageNumber = 1, int pageSize = 10, List<FilterPropsRequest>? filters = null)
    {
        return Ok(await _mediator.Send(new NotAssignedApplicantReportQuery(pageNumber, pageSize, filters)));
    }
}