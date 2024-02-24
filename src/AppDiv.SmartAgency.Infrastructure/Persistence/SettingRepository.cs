using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class SettingRepository : BaseRepository<Setting>, ISettingRepository
    {
        private readonly SmartAgencyDbContext _context;
        public SettingRepository(SmartAgencyDbContext context) : base(context)
        {
            _context = context;
        }

        async Task<Setting> ISettingRepository.GetSettingByKey(string key)
        {
            return await base.GetAsync(key);
        }
        async Task<Setting> ISettingRepository.GetByIdAsync(Guid id)
        {
            return await base.GetAsync(id);
        }
    }
}