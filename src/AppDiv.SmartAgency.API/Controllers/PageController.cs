using AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Pagess;
using AppDiv.SmartAgency.Application.Features.Pages.Command.Create;
using AppDiv.SmartAgency.Application.Features.Pages.Command.Delete;
using AppDiv.SmartAgency.Application.Features.Pages.Command.Update;
using AppDiv.SmartAgency.Application.Features.Pages.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers
{
    public class PageController(IMediator mediator) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        public async Task<ActionResult<PageResponseDTO>> CreatePage(CreatePageRequest pageRequest, CancellationToken token)
        {
            var response = await _mediator.Send(new CreatePageCommand(pageRequest), token);
            return Ok(response);
        }

        [HttpGet("get-all-pages")]
        public async Task<ActionResult<PageResponseDTO>> GetAllPages(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
        {
            return Ok(await _mediator.Send(new GetAllPagesQuery(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
        }


        [HttpGet("{id}")]
        public async Task<GetPageByIdResponseDTO> Get(Guid id)
        {
            return await _mediator.Send(new GetPageByIdQuery(id));
        }

        [HttpDelete("delete/{ids}")]
        public async Task<ActionResult> DeletePages([FromQuery] DeletePagesCommand cmd)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(cmd);
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }


        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditPageCommand command)
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
        }


    }
}