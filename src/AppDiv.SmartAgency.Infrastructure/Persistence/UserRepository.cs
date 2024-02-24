
using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class UserRepository(SmartAgencyDbContext context) : BaseRepository<ApplicationUser>(context), IUserRepository
{
    private readonly SmartAgencyDbContext _context = context;

    public async Task<SearchModel<ApplicationUser>> GetPaginatedUser(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection, Expression<Func<ApplicationUser, bool>>? predicate = null, params string[] eagerLoadedProperties)
    {
        var response = new SearchModel<ApplicationUser>();



        return response;
    }
}