

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class TraveledApplicantRepository : BaseRepository<TraveledApplicant>, ITraveledApplicantRepository
{
    public TraveledApplicantRepository(SmartAgencyDbContext dbContext) : base(dbContext) { }
}