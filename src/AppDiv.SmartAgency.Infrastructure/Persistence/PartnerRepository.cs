using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
{
    private readonly SmartAgencyDbContext _context;
    public PartnerRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(Partner partner, CancellationToken cancellationToken)
    {
        await base.InsertAsync(partner, cancellationToken);
    }
    public async Task<Partner> GetByIdAsync(string Id)
    {
        return await base.GetAsync(Id);
    }
    
}
}