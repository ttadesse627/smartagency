
using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Features.Complaints.Command.Create;
using AppDiv.SmartAgency.Application.Features.Complaints.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [Route("api/complaint")]
    [ApiController]
    public class ComplaintController : Controller
    {
        private readonly IMediator _mediator;
        public ComplaintController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("send-complaint")]
        public async Task<ActionResult<List<GetComplaintResponseDTO>>> SendComplaint([FromBody] OrderComplaintRequest request)
        {
            return Ok(await _mediator.Send(new SendOrderComplaintCommand(request)));
        }

        [HttpGet("get-complaints")]
        public async Task<ActionResult<List<GetAllComplaintsResponseDTO>>> GetComplaints(OrderComplaintRequest request)
        {
            return Ok(await _mediator.Send(new GetAllComplaintsQuery()));
        }
    }
}