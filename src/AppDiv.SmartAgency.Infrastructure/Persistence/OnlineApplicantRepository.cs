using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class OnlineApplicantRepository: BaseRepository<OnlineApplicant>, IOnlineApplicantRepository
{
    private readonly SmartAgencyDbContext _context;
    public OnlineApplicantRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(OnlineApplicant onlineApplicant, CancellationToken cancellationToken)
    {
        await base.InsertAsync(onlineApplicant, cancellationToken);
    }
    public async Task<OnlineApplicant> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }
    
   
}
}