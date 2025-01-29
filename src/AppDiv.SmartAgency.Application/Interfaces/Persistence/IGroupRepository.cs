
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IGroupRepository : IBaseRepository<UserGroup>
    {
        Task<List<UserGroup>> GetMultipleUserGroups(ICollection<Guid> groupIds);
        Task<List<UserGroup>> GetUserGroupByUserId(string userId);
        IQueryable<UserGroup> GetMultipleUserGroupsBySearch(string? searchTerm);
        Task<bool> CheckPermissionsAsync(string userId, string permissionName, PermissionEnum action);
    }
}