
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface ISettingRepository : IBaseRepository<Setting>
    {
        Task<Setting?> GetByIdAsync(Guid id);
        Task<Setting?> GetSettingByKey(string key);


    }
}