
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
    
     [HttpGet]
     public async Task<ActionResult<NewAssignedVisaResponseDTO>> GetNewAssignedVisa()
     {
            var result = await _mediator.Send(new GetNewAssignedVisaQuery());
            return Ok(result);
     } 

     [HttpGet] 
     public async Task<ActionResult<NotAssignedVisaResponseDTO>> GetNotAssignedVisa()
     {
            var result = await _mediator.Send(new GetNotAssignedVisaQuery());
            return Ok(result);
     }
     [HttpGet]
    public async Task<ActionResult<NotProcessedApplicantResponseDTO>> GetNotProcessedApplicant()
    {
        var result = await _mediator.Send(new GetNotProcessedApplicantQuery());
        return Ok(result);    
    }
    [HttpGet]
    public async Task<ActionResult<VisaExpiryResponseDTO>> GetExpiredVisa()
    {
        var result = await _mediator.Send(new GetExpiredVisaQuery());
        return Ok(result);
    }
     public async Task<ActionResult<PenalityResponseDTO>>  GetPenality()
     {
        var result = await _mediator.Send(new GetPenalityQuery());
        return Ok(result);
     }

        
    }
}