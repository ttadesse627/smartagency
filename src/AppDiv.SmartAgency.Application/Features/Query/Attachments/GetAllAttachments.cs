using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Attachments
{
    public class GetAllAttachments : IRequest<List<AttachmentResponseDTO>>
    {
    }
}