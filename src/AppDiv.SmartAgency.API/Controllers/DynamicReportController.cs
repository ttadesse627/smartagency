using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Features.Reports.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class DynamicReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DynamicReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("get")]
        public async Task<ActionResult<List<Object>>> getDaynamicViewData(ReportsQueryRequest query)
        {
            //var result = string.Empty;

            var result = await _mediator.Send(new GetAllReportsQuery(query));
            return Ok(result);
        }




    }
}