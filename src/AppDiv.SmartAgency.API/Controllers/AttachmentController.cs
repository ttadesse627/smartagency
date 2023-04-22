

using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
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
}