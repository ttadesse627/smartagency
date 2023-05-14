

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Delete.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Update.Attachments;
using AppDiv.SmartAgency.Application.Features.Query.Attachments;
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
    public async Task<ActionResult<CreateAttachmentResponseDTO>> CreateAttachment(CreateAttachmentCommand attachmentRequest, CancellationToken token)
    {
        var response = await _mediator.Send(attachmentRequest);
        return Ok(response);
    }
    [HttpGet("get-all")]
    public async Task<ActionResult<AttachmentResponseDTO>> GetAllAttachments(int pageNumber = 1, int pageSize = 20, string searchTerm = "", string? searchByColumnName = null, string? orderBy = null, SortingDirection sortingDirection = SortingDirection.Ascending)
    {
        return Ok(await _mediator.Send(new GetAllAttachments(pageNumber, pageSize, searchTerm, searchByColumnName, orderBy, sortingDirection)));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<ServiceResponse<DeleteAttachmentCommand>>> DeleteAttachment(Guid id)
    {
        var result = new ServiceResponse<List<AttachmentResponseDTO>>();
        try
        {
            result = await _mediator.Send(new DeleteAttachmentCommand { Id = id });
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
    public async Task<ActionResult<ServiceResponse<EditAttachmentCommand>>> EditAttachment(Guid id, [FromBody] EditAttachmentCommand request)
    {
        try
        {
            if (request.Id == id)
            {
                var result = await _mediator.Send(new EditAttachmentCommand
                {
                    Id = request.Id,
                    Code = request.Code,
                    Description = request.Description,
                    Category = request.Category,
                    IsRequired = request.IsRequired,
                    ShowOnCv = request.ShowOnCv
                }
                );
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