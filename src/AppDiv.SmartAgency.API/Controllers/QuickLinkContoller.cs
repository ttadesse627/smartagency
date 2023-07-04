
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Features.QuickLinks.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

    [ApiController]
    [Route("api/quick-links")]
    public class QuickLinkContoller : ControllerBase
    {


        private readonly IMediator _mediator;

        public QuickLinkContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("a")]
        public async Task<ActionResult<NewAssignedVisaResponseDTO>> GetNewAssignedVisa()
        {
            var result = await _mediator.Send(new GetNewAssignedVisaQuery());
            return Ok(result);
        }

        [HttpGet("b")]
        public async Task<ActionResult<NotAssignedVisaResponseDTO>> GetNotAssignedVisa()
        {
            var result = await _mediator.Send(new GetNotAssignedVisaQuery());
            return Ok(result);
        }
        [HttpGet("c")]
        public async Task<ActionResult<NotProcessedApplicantResponseDTO>> GetNotProcessedApplicant()
        {
            var result = await _mediator.Send(new GetNotProcessedApplicantQuery());
            return Ok(result);
        }
        [HttpGet("d")]
        public async Task<ActionResult<VisaExpiryResponseDTO>> GetExpiredVisa()
        {
            var result = await _mediator.Send(new GetExpiredVisaQuery());
            return Ok(result);
        }
        [HttpGet("e")]
        public async Task<ActionResult<PenalityResponseDTO>> GetPenality()
        {
            var result = await _mediator.Send(new GetPenalityQuery());
            return Ok(result);
        }

    [HttpGet("get-complaint")]
     public async Task<ActionResult<ComplaintResponseDTO>> GetComplaints()
     {
          var result = await _mediator.Send(new GetComplaintQuery());
          return Ok(result);   
     }

     [HttpGet("get-daynamic-process")]
     public async Task<ActionResult<DynamicProcessResponseDTO>> GetDynamicProcesses()
     {
           var result= await _mediator.Send(new GetDynamicProcessQuery());
           return Ok(result);
     }



        
    }
}