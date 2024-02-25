
using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Contracts;
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
            queryableUsers = _context.ApplicationUsers.Where(predicate).AsQueryable();
        }
        else
        {
            queryableUsers = _context.ApplicationUsers.AsQueryable();
        }

        foreach (var nav_property in eagerLoadedProperties)
        {
            queryableUsers = queryableUsers.Include(nav_property);
        }

        return queryableUsers;
    }
}

