
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class TicketRegistrationRepository : BaseRepository<TicketRegistration>, ITicketRegistrationRepository
{
    private readonly SmartAgencyDbContext _context;
    public TicketRegistrationRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }
}