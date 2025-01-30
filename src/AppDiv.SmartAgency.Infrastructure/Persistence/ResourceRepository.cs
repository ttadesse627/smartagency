using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ResourceRepository(SmartAgencyDbContext dbContext) : BaseRepository<Resource>(dbContext), IResourceRepository
{
    private readonly SmartAgencyDbContext _context = dbContext;
    public async Task<Resource?> GetByIdAsync(Guid id)
    {
        return await _context.Resources.FindAsync(id);
    }
}
