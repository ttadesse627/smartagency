
using AppDiv.CRVS.Application.Interfaces.Persistence.Settings;
using AppDiv.CRVS.Domain.Entities.Settings;
using AppDiv.CRVS.Infrastructure.Context;

namespace AppDiv.CRVS.Infrastructure.Persistence.Settings
{
    public class SuffixRepository : BaseRepository<Suffix>, ISuffixRepository
    {
        public SuffixRepository(CRVSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
