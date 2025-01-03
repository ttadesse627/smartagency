

using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface ILookUpRepository : IBaseRepository<LookUp>
    {
        Task<LookUp?> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(LookUp lookUp);
        Task<List<LookUp>> GetAllKeysAsync(List<string> keys);
    }
}