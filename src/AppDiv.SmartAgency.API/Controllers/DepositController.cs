



using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Features.Command.Create.Deposits;
using AppDiv.SmartAgency.Application.Features.Command.Create.Partners;
using AppDiv.SmartAgency.Application.Features.Command.Delete.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Update.Partners;
using AppDiv.SmartAgency.Application.Features.Query.Customers;
using AppDiv.SmartAgency.Application.Features.Query.Deposits;
using AppDiv.SmartAgency.Application.Features.Query.Partners;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

[ApiController]
[Route("api/deposit")]
    public class DepositController: ControllerBase
{
    private readonly IMediator _mediator;
    public DepositController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<DepositResponseDTO>> CreateDeposit(CreateDepositCommand depositRequest, CancellationToken token)
    {
        var response = await _mediator.Send(depositRequest);
        return Ok(response);
    }

   [HttpGet("get-all-deposits")]
    public async Task<ActionResult<DepositResponseDTO>> GetAllDeposits()
    {
        return Ok(await _mediator.Send(new GetAllDepositQuery()));
    }
/*
     [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<DepositResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetDepositByIdQuery(id));
        }
 
      [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteDeposit(Guid id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteDepositCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }



     
        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditPartnerCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

                //var result = await _mediator.Send(command,id);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
          
  
}*/
}

}