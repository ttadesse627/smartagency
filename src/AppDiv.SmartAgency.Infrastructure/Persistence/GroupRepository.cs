using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class GroupRepository(SmartAgencyDbContext dbContext) : BaseRepository<UserGroup>(dbContext), IGroupRepository
    {
        private readonly SmartAgencyDbContext _context = dbContext;

        public async Task<bool> CheckPermissionsAsync(string userId, string permissionName, PermissionEnum action)
        {
            var permissions = await _context.Permissions
                            .Where(p => p.UserGroup.AppUsers.Any(user => user.Id == userId) && p.Resource.Name == permissionName).ToListAsync();


            return permissions.Any(p => p.Actions.Contains(action));
        }

        public async Task<List<UserGroup>> GetMultipleUserGroups(ICollection<Guid> groupIds)
        {
            return await _context.UserGroups.Where(ug => groupIds.Contains(ug.Id)).ToListAsync();
        }
        public IQueryable<UserGroup> GetMultipleUserGroupsBySearch(string? searchTerm)
        {
            return !string.IsNullOrEmpty(searchTerm) ? _context.UserGroups
                .Include(ug => ug.Permissions)
                .Where(ug => ug.Name.Contains(searchTerm)) :
                _context.UserGroups
                .Include(ug => ug.Permissions);
        }

        public async Task<List<UserGroup>> GetUserGroupByUserId(string userId)
        {
            var response = await _context.UserGroups.Where(ug => ug.AppUsers.Any(apU => apU.Id == userId)).ToListAsync();
            return response;
        }
    }
}


