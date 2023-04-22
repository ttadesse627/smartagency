using System.Linq;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class AttaachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
{
    private readonly SmartAgencyDbContext _context;
    public AttaachmentRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { }

    public override async Task InsertAsync(Attachment attachment, CancellationToken cancellationToken)
    {
        await base.InsertAsync(attachment, cancellationToken);
    }
    public async Task<Attachment> GetByIdAsync(string Id)
    {
        return await base.GetAsync(Id);
    }
}