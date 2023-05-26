using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class LookUpRepository : BaseRepository<LookUp>, ILookUpRepository
    {
        private readonly SmartAgencyDbContext _context;
        public LookUpRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public override async Task InsertAsync(LookUp lookUp, CancellationToken cancellationToken)
        {
            await base.InsertAsync(lookUp, cancellationToken);
        }
        public async Task<LookUp> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }
        public async Task<Int32> UpdateAsync(LookUp lookUp)
        {

                   _context.LookUps.Update(lookUp);  
                   return await _context.SaveChangesAsync();
                     
        }

    }
}