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
        public async Task<LookUp?> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }
        public async Task<int> UpdateAsync(LookUp lookUp)
        {

            _context.LookUps.Update(lookUp);
            return await _context.SaveChangesAsync();

        }

        public async Task<List<LookUp>> GetAllKeysAsync(List<string> keys)
        {
            var query = _context.LookUps.AsQueryable();

            if (keys != null && keys.Count > 0)
            {
                query = query.Where(lk => keys.Contains(lk.Category));
            }

            var result = await query.ToListAsync();
            return result;
        }

    }
}