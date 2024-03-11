using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;
using AppDiv.SmartAgency.Application.Features.Deposits.Command.Create;
using AppDiv.SmartAgency.Application.Features.Deposits.Command.Delete;
using AppDiv.SmartAgency.Application.Features.Deposits.Command.Update;
using AppDiv.SmartAgency.Application.Features.Deposits.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class DepositController(IMediator mediator) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        public async Task<ActionResult<DepositResponseDTO>> CreateDeposit(CreateDepositRequest depositRequest, CancellationToken token)
        {
            var response = await _mediator.Send(new CreateDepositCommand(depositRequest), token);
            return Ok(response);
        }

        [HttpGet("get-all-deposits")]
        public async Task<ActionResult<DepositResponseDTO>> GetAllDeposits(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllDepositQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }

        [HttpGet("{id}")]
        public async Task<CreateDepositRequest> Get(Guid id)
        {
            return await _mediator.Send(new GetDepositByIdQuery(id));
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteDeposits([FromQuery] List<Guid> ids)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteDepositCommand(ids));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditDepositCommand command)
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