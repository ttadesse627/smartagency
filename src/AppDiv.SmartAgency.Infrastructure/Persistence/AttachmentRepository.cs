using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class AttaachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
{
    public AttaachmentRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {}

    async Task<Attachment> GetByIdAsync(string Id)
    {
        return await base.GetAsync(Id);
    }
    async Task<Attachment> GetByCodeAsync(string Code)
    {
        return;
    }

}