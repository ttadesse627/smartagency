using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class UserRepository(SmartAgencyDbContext context) : BaseRepository<ApplicationUser>(context), IUserRepository
{
    private readonly SmartAgencyDbContext _context = context;

    public async Task<IQueryable<ApplicationUser>> GetQueryableUsers(string searchTerm, Expression<Func<ApplicationUser, bool>>? predicate = null, params string[] eagerLoadedProperties)
    {
        await Task.CompletedTask;
        IQueryable<ApplicationUser> queryableUsers;

        if (predicate is not null)
        {
            queryableUsers = _context.Users.Where(predicate).AsQueryable();
        }
        else
        {
            queryableUsers = _context.Users.AsQueryable();
        }

        foreach (var nav_property in eagerLoadedProperties)
        {
            queryableUsers = queryableUsers.Include(nav_property);
        }

        return queryableUsers;
    }

    public async Task<List<Permission>> GetUserPermissionsAsync(string userId, string controllerName)
    {
        List<Permission> permissions = [];
        ApplicationUser? user = await _context.Users.Include(user => user.UserGroups).ThenInclude(ug => ug.Permissions).FirstOrDefaultAsync(user => user.Id == userId);
        if (user is not null)
        {
            permissions = [.. user.UserGroups.SelectMany(ug => ug.Permissions).Where(per => per.Resource.Name == controllerName)];
        }
        else throw new NotFoundException("User", userId);
        return permissions;
    }
    public async Task<bool> PermissionExists(string userId, string controllerName)
    {
        var permissionExists = await _context.Users.Include(user => user.UserGroups)
                            .ThenInclude(ug => ug.Permissions)
                            .ThenInclude(p => p.Resource)
                            .AnyAsync(user => user.Id == userId && user.UserGroups.SelectMany(ug => ug.Permissions).Any(per => per.Resource.Name == controllerName));

        return permissionExists;
    }
}
