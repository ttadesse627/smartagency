
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class TicketRefundRepository : BaseRepository<TicketRefund>, ITicketRefundRepository
{
    private readonly SmartAgencyDbContext _context;
    public TicketRefundRepository(SmartAgencyDbContext context) : base(context)
    {
        _context = context;
    }
}