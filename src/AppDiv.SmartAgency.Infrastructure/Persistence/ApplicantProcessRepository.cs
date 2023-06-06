
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class ApplicantProcessRepository : BaseRepository<ApplicantProcess>, IApplicantProcessRepository
    {
        private readonly SmartAgencyDbContext _context;
        public ApplicantProcessRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

    }
}