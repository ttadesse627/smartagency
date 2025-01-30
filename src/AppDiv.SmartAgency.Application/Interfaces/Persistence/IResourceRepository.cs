using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IResourceRepository : IBaseRepository<Resource>
    {
        Task<Resource?> GetByIdAsync(Guid id);
    }
}