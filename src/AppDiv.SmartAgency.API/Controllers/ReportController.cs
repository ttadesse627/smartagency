using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Features.Reports.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IReportsRepository _reportRepository;

        public ReportController(IMediator mediator, IReportsRepository reportsRepository)
        {
            _mediator = mediator;
            _reportRepository = reportsRepository;
        }

        [HttpGet("get-reports")]
        public async Task<ActionResult<JObject>> getReports()
        {
            return Ok(await _reportRepository.GetReports());
        }

        // [HttpPost("create-report")]
        // public async Task<ActionResult<ServiceResponse<Int32>>> CreateAsync(string reportName, string reportType, ReportsQueryRequest query)
        // {
        //     var result = await _mediator.Send(new CreateReportCmd(reportName, reportType, query));
        //     return Ok(result);
        // }
        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<Int32>>> CreateReportAsync([FromBody] CreateReportRequest createReport)
        {
            var result = await _mediator.Send(new CreateReportCommand(createReport.ReportName, createReport.ReportQuery));
            return Ok(result);
        }

        [HttpPost("get/{reportName}")] //Task<(IEnumerable<Dictionary<string, object>>, List<String>)>
        public async Task<ActionResult<JObject>> getDaynamicViewData(string reportName, ReportsQueryRequest? query = null)
        {
            var result = await _mediator.Send(new GetAllReportsQuery(reportName, query));
            return Ok(result);
        }


        // [HttpGet("get-test")]
        // public async Task<ActionResult<JObject>> getTestData()
        // {
        //     return Ok(await _reportRepository.GetTestData());
        // }

    }
}