

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    private readonly SmartAgencyDbContext _context;
    public UserRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }
}