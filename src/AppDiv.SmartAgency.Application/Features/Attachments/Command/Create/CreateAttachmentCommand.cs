
using AppDiv.SmartAgency.Application.Contracts.Request.Attachments;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public record CreateAttachmentCommand(CreateAttachmentRequest attachment) : IRequest<CreateAttachmentCommandResponse>
{

}