
using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Features.Complaints.Command.Create;
using AppDiv.SmartAgency.Application.Features.Complaints.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class ComplaintController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("send-complaint")]
        public async Task<ActionResult<GetOrderComplaintsResponseDTO
        >> SendComplaint([FromBody] OrderComplaintRequest request)
        {
            return Ok(await _mediator.Send(new SendOrderComplaintCommand(request)));
        }

        [HttpGet("get-complaints")]
        public async Task<ActionResult<List<GetAllComplaintsResponseDTO>>> GetComplaints()
        {
            return Ok(await _mediator.Send(new GetAllComplaintsQuery()));
        }

        [HttpGet("get-order-complaints/{applicantId}")]
        public async Task<ActionResult<List<GetAllComplaintsResponseDTO>>> GetOrderComplaints(Guid applicantId)
        {
            return Ok(await _mediator.Send(new GetOrderComplaintsQuery(applicantId)));
        }
    }
}