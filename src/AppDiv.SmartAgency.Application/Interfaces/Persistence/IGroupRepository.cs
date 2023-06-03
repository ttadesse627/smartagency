
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IGroupRepository : IBaseRepository<UserGroup>
    {
        // Task<UserGroup> GetByIdAsync(Guid id);
        Task<ICollection<UserGroup>> GetMultipleUserGroups(ICollection<Guid> groupIds);
    }
}