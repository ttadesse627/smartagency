using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class RequestedApplicantRepository : BaseRepository<RequestedApplicant>, IRequestedApplicantRepository
    {
        private readonly SmartAgencyDbContext _context;

        public RequestedApplicantRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}