using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class EnjazRepository : BaseRepository<Enjaz>, IEnjazRepository
{
    private readonly SmartAgencyDbContext _context;
    public EnjazRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }
    // public async Task<Category> GetByIdAsync(Guid Id)
    // {
    //     return await base.GetAsync(Id);
    // }
    
}
}