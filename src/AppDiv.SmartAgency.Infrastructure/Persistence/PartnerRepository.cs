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
    public async Task<Partner> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }
    
   public async Task<Int32> UpdateAsync(Partner partner)
   {
      
        _context.Partners.Update(partner);
        var response = await _context.SaveChangesAsync();

        return response;
    }
}
}