
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IGroupRepository : IBaseRepository<UserGroup>
    {
        Task<List<UserGroup>> GetMultipleUserGroups(ICollection<Guid> groupIds);
        Task<List<UserGroup>> GetUserGroupByUserId(string userId);
    }
}