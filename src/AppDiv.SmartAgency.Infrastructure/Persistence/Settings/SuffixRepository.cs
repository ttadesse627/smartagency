
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Settings;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence.Settings
{
    public class SuffixRepository : BaseRepository<Suffix>, ISuffixRepository
    {
        public SuffixRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
