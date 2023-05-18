
using AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
public record CreateAttachmentCommand(CreateAttachmentRequest attachment) : IRequest<CreateAttachmentCommandResponse>
{

}