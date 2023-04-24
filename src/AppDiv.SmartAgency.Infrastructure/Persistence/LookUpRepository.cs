using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class LookUpRepository : BaseRepository<LookUp>, ILookUpRepository
{
    private readonly SmartAgencyDbContext _context;
    public LookUpRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
         _context=dbContext ; 
    }

    public override async Task InsertAsync(LookUp lookUp, CancellationToken cancellationToken)
    {
        await base.InsertAsync(lookUp, cancellationToken);
    }
    public async Task<LookUp> GetByIdAsync(string Id)
    {
        return await base.GetAsync(Id);
    }
    
}
}