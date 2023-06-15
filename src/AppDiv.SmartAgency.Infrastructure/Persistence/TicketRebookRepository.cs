
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class TicketRebookRepository : BaseRepository<TicketRebook>, ITicketRebookRepository
{
    private readonly SmartAgencyDbContext _context;
    public TicketRebookRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }
}