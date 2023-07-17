



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
    }

}