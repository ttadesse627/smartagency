

using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IUserRepository : IBaseRepository<ApplicationUser>
{
    // Task<IQueryable<ApplicationUser>> GetQueryableUsers(string searchTerm, Expression<Func<ApplicationUser, bool>>? predicate = null, params string[] eagerLoadedProperties);
    Task<List<Permission>> GetUserPermissionsAsync(string userId, string controllerName);
    Task<bool> PermissionExists(string userId, string controllerName);
}
