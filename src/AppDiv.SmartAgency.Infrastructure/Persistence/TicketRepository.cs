
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class TicketReadyRepository : BaseRepository<TicketReady>, ITicketReadyRepository
{
    private readonly SmartAgencyDbContext _context;
    public TicketReadyRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }
}