
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
         _mediator= mediator;
       }
    
     [HttpGet("get-new-assigned-visa")]
     public async Task<ActionResult<NewAssignedVisaResponseDTO>> GetNewAssignedVisa()
     {
            var result = await _mediator.Send(new GetNewAssignedVisaQuery());
            return Ok(result);
     } 

     [HttpGet("get-not-assigned-visa")] 
     public async Task<ActionResult<NotAssignedVisaResponseDTO>> GetNotAssignedVisa()
     {
            var result = await _mediator.Send(new GetNotAssignedVisaQuery());
            return Ok(result);
     }
     [HttpGet("get-not-processed-applicant")]
    public async Task<ActionResult<NotProcessedApplicantResponseDTO>> GetNotProcessedApplicant()
    {
        var result = await _mediator.Send(new GetNotProcessedApplicantQuery());
        return Ok(result);    
    }
    [HttpGet("get-expired-visa")]
    public async Task<ActionResult<VisaExpiryResponseDTO>> GetExpiredVisa()
    {
        var result = await _mediator.Send(new GetExpiredVisaQuery());
        return Ok(result);
    }
    [HttpGet("get-penality")]
     public async Task<ActionResult<PenalityResponseDTO>>  GetPenality()
     {
        var result = await _mediator.Send(new GetPenalityQuery());
        return Ok(result);
     }

        
    }
}