

using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IUserRepository : IBaseRepository<ApplicationUser>
{
    // Task<IQueryable<ApplicationUser>> GetQueryableUsers(string searchTerm, Expression<Func<ApplicationUser, bool>>? predicate = null, params string[] eagerLoadedProperties);
}
