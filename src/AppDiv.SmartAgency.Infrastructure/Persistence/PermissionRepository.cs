using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class PermissionRepository(SmartAgencyDbContext dbContext) : BaseRepository<LookUp>(dbContext), IPermissionRepository
    {
        private readonly SmartAgencyDbContext _context = dbContext;

        public async Task<HashSet<string>> GetPermissionAsync(string userId)
        {
            ICollection<UserGroup>[] userGroups = await _context.Set<ApplicationUser>()
                .Include(user => user.UserGroups)
                .ThenInclude(ug => ug.Permissions)
                .Where(user => user.Id == userId)
                .Select(user => user.UserGroups)
                .ToArrayAsync();

            return userGroups
                .SelectMany(ug => ug)
                .SelectMany(ug => ug.Permissions)
                .Select(permission => permission.Name)
                .ToHashSet();


        }
    }
}