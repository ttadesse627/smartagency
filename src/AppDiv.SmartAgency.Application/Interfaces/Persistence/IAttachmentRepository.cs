

using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IAttachmentRepository : IBaseRepository<Attachment>
{
    Task<Attachment> GetByIdAsync(string Id);
    Task<Attachment> GetByCodeAsync(string Code);
}