

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Features.Command.Create.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Delete.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Update.LookUps;
using AppDiv.SmartAgency.Application.Features.Query.LookUps;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{

[ApiController]
[Route("api/lookUp")]
    public class LookUpController: ControllerBase
{
    private readonly IMediator _mediator;
    public LookUpController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateLookUpResponseDTO>> CreateLookUp(CreateLookUpCommand lookUpRequest, CancellationToken token)
    {
        var response = await _mediator.Send(lookUpRequest);
        return Ok(response);
    }

    [HttpGet("get-all-lookup")]
    public async Task<ActionResult<LookUpResponseDTO>> GetAllLookUps()
    {
        return Ok(await _mediator.Send(new GetAllLookUps()));
    }

     [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<LookUp> Get(string id)
        {
            return await _mediator.Send(new GetLookUpByIdQuery(id));
        }

      [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteLookUp(string id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteLookUpCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }



     
        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(string id, [FromBody] EditLookUpCommand command)
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
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
          
  
}
}

}