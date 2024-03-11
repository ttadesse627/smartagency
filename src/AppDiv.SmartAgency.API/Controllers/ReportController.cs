using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Features.Reports.Command.Create;
using AppDiv.SmartAgency.Application.Features.Reports.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class ReportController(IMediator mediator, IReportsRepository reportsRepository) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IReportsRepository _reportRepository = reportsRepository;

        [HttpGet("get-reports")]
        public async Task<ActionResult<JObject>> GetReports()
        {
            return Ok(await _reportRepository.GetReports());
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateReportAsync([FromBody] CreateReportRequest createReport)
        {
            var result = await _mediator.Send(new CreateReportCommand(createReport.ReportName, createReport.ReportQuery));
            return Ok(result);
        }

        [HttpPost("get/{reportName}")]
        public async Task<ActionResult<JObject>> GetDaynamicViewData(string reportName, ReportsQueryRequest? query = null)
        {
            var response = new JObject();
            if (!string.IsNullOrEmpty(reportName) || !string.IsNullOrWhiteSpace(reportName))
            {
                response = await _mediator.Send(new GetAllReportsQuery(reportName, query));
            }
            return Ok(response);
        }
    }
}