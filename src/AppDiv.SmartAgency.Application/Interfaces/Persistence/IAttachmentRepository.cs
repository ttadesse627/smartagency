

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IAttachmentRepository : IBaseRepository<Attachment>
{
    Task<Attachment> GetByIdAsync(Guid id);
    Task<ServiceResponse<List<AttachmentResponseDTO>>> DeleteAttachment(Guid id);
    Task<ServiceResponse<AttachmentResponseDTO>> UpdateAttachment(EditAttachmentCommand request);
}