using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Features.Dashbourds.Query;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    [ApiController]
    [Route("api/dashbourd")]
    public class DashbourdController: ControllerBase
    {

        private readonly  IMediator _mediator;                 
        public DashbourdController(IMediator mediator)
        {
            _mediator=mediator;
            
        }

       [HttpGet("get")]

       public async Task<ActionResult<Dictionary<string, Object>>> GetDashbourdInformation([FromQuery]DateTime? startDate = null, DateTime? endDate=null)
       { 
          
           return await _mediator.Send(new GetDashbourdQuery(startDate, endDate));
     
       } 




        
    }
}