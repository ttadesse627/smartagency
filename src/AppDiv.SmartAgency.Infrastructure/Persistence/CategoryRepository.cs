using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly SmartAgencyDbContext _context;
    public CategoryRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }
    public async Task<Category> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }
    
}
}