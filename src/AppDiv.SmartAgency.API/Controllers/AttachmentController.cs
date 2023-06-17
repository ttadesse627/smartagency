

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Delete;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;
using AppDiv.SmartAgency.Application.Features.Attachments.Query;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.API.Controllers;

[ApiController]
[Route("api/attachment")]
public class AttachmentController : ControllerBase
{
    private readonly IMediator _mediator;
    public AttachmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<int>>> CreateAttachment(CreateAttachmentRequest attachmentRequest, CancellationToken token)
    {
        var response = await _mediator.Send(new CreateAttachmentCommand(attachmentRequest));
        return Ok(response);
    }
    [HttpGet("get-all")]
    public async Task<ActionResult<AttachmentResponseDTO>> GetAllAttachments(int pageNumber = 1, int pageSize = 10, string? searchTerm = "", string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetAllAttachments(pageNumber, pageSize, searchTerm, orderBy, sortingDirection)));
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<AttachmentResponseDTO>> GetByIdAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetAttachmentQuery(id)));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteAttachment(Guid id)
    {
        var result = new ServiceResponse<int>();
        try
        {
            result = await _mediator.Send(new DeleteAttachmentCommand(id));
            result.Message = $"The attachment with id {id} is successfully deleted!";
            result.Success = true;
        }
        catch (System.Exception ex)
        {
            result.Message = ex.Message;
            result.Success = false;
        }

        if (result.Success)
        {
            return Ok(result);
        }
        else return BadRequest(result);
    }

    [HttpPut("edit/{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> EditAttachment(Guid id, [FromBody] EditAttachmentCommand request)
    {
        var result = new ServiceResponse<int>();
        try
        {
            if (request.Id == id)
            {
                result = await _mediator.Send(request);
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

    [HttpGet("lookup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<AttachmntResponseDTO> GetDropdown()
    {
        return await _mediator.Send(new GetDropDownAttachmentsQuery());
    }
}