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

        [HttpGet("get-objects")]
        public async Task<ActionResult<JObject>> getReportObjects()
        {
            return Ok(await _reportRepository.GetReportObjects());
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<Int32>>> CreateReportAsync(string reportName, string reportType, ReportsQueryRequest query)
        {
            var result = await _mediator.Send(new CreateReportCommand(reportName, reportType, query));
            return Ok(result);
        }

        [HttpPost("get/{reportName}")] //Task<(IEnumerable<Dictionary<string, object>>, List<String>)>
        public async Task<ActionResult<JObject>> getDaynamicViewData(string reportName, ReportsQueryRequest query)
        {
            var result = await _mediator.Send(new GetAllReportsQuery(reportName, query));
            return Ok(result);
        }


        [HttpGet("get-test")]
        public async Task<ActionResult<JObject>> getTestData()
        {
            return Ok(await _reportRepository.GetTestData());
        }

    }
}