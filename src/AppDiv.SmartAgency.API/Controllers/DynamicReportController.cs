using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Features.Reports.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class DynamicReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGetReportsRepository _reportRepository;

        public DynamicReportController(IMediator mediator, IGetReportsRepository reportsRepository)
        {
            _mediator = mediator;
            _reportRepository = reportsRepository;
        }

        [HttpGet("get-objects")]
        public async Task<ActionResult<List<DbTable>>> getReportObjects()
        {
            var result = await _mediator.Send(new GetReportObjectsQuery());
            return Ok(result);
        }

        [HttpPost("get/{reportName}")] //Task<(IEnumerable<Dictionary<string, object>>, List<String>)>
        public async Task<ActionResult<(IEnumerable<Dictionary<string, object>>, List<String>)>> getDaynamicViewData(string reportName, ReportsQueryRequest query)
        {
            var result = await _mediator.Send(new GetAllReportsQuery(reportName, query));
            return Ok(result);
        }

        [HttpGet("get-test")]
        public async Task<ActionResult<IEnumerable<Dictionary<string, object>>>> getTestData()
        {
            return Ok(await _reportRepository.GetTestData());
        }

    }
}