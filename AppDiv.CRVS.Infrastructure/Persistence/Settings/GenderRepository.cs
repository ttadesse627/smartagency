using AppDiv.CRVS.Domain.Entities.Settings;
using AppDiv.CRVS.Infrastructure.Context;
using AppDiv.CRVS.Interfaces.Persistence.Settings;

namespace AppDiv.CRVS.Infrastructure.Persistence.Settings
{
    public class GenderRepository : BaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(CRVSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
