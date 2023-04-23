

using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Attachments;
public class GetAttachmentByCode : IRequest<Attachment>
{
    public string Code { get; private set; }
    public GetAttachmentByCode(string Code)
    {
        this.Code = Code;
    }
}