

using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface ILookUpRepository : IBaseRepository<LookUp>
    {
        Task<LookUp> GetByIdAsync(string id);
    }
}