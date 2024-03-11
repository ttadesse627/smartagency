using AppDiv.SmartAgency.Application.Features.Dashbourds.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class DashbourdController(IMediator mediator) : ApiControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpGet("get")]
        public async Task<ActionResult<Dictionary<string, object>>> GetDashbourdInformation([FromQuery] DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _mediator.Send(new GetDashbourdQuery(startDate, endDate));
        }








    }
}