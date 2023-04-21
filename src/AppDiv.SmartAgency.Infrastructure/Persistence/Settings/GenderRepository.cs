using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Interfaces.Persistence.Settings;

namespace AppDiv.SmartAgency.Infrastructure.Persistence.Settings
{
    public class GenderRepository : BaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
