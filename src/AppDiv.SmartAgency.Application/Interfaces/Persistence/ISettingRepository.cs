using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface ISettingRepository : IBaseRepository<Setting>
    {
        Task<IEnumerable<Setting>> GetAllAsync();
        Task<Setting> GetByIdAsync(Guid id);
        Task<Setting> GetSettingByKey(string key);
        //Task InitializeSettingCouch();


    }
}