



using AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
using AppDiv.SmartAgency.Application.Features.Enjazs.Command.Create;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AppDiv.SmartAgency.Application.Contracts.DTOs.EnjazDTOs;
using AppDiv.SmartAgency.Application.Features.Enjazs.Query;
using AppDiv.SmartAgency.Application.Features.Enjazs.Command.Delete;

namespace AppDiv.SmartAgency.API.Controllers
{

    [ApiController]
    [Route("api/enjaz")]
    public class EnjazController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EnjazController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateEnjaz(CreateEnjazRequest request, CancellationToken token)
        {
            var response = await _mediator.Send(new CreateEnjazCommand(request));
            return Ok(response);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<SearchModel<EnjazResponseDTO>>> GetAllEnjazs(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllEnjazsQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteEnjaz(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteEnjazCommand(id)));

        }


        // [HttpGet("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<CreateDepositRequest> Get(Guid id)
        // {
        //     return await _mediator.Send(new GetDepositByIdQuery(id));
        // }


        // [HttpDelete("Delete/{id}")]
        // public async Task<ActionResult> DeleteDeposit(Guid id)
        // {
        //     try
        //     {
        //         string result = string.Empty;
        //         result = await _mediator.Send(new DeleteDepositCommand(id));
        //         return Ok(result);
        //     }
        //     catch (Exception exp)
        //     {
        //         return BadRequest(exp.Message);
        //     }
        // }




        // [HttpPut("Edit/{id}")]
        // public async Task<ActionResult> Edit(Guid id, [FromBody] EditDepositCommand command)
        // {
        //     try
        //     {
        //         if (command.Id == id)
        //         {
        //             var result = await _mediator.Send(command);
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return BadRequest();
        //         }

        //         //var result = await _mediator.Send(command,id);
        //     }
        //     catch (Exception exp)
        //     {
        //         return BadRequest(exp.Message);
        //     }


        // }
    }

}