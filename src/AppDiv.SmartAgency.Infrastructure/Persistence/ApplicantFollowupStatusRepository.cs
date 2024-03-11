using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class ApplicantFollowupStatusRepository(SmartAgencyDbContext dbContext) : BaseRepository<ApplicantFollowupStatus>(dbContext), IApplicantFollowupStatusRepository
    {
        private readonly SmartAgencyDbContext _context = dbContext;

        public override async Task InsertAsync(ApplicantFollowupStatus applicantFollowupStatus, CancellationToken cancellationToken)
        {

            await base.InsertAsync(applicantFollowupStatus, cancellationToken);
        }
        public async Task<ApplicantFollowupStatus> GetByIdAsync(Guid Id)
        {
            var followupStatus = await _context.ApplicantFollowupStatuses
                         .Include(a => a.Applicant)
                         .Include(a => a.FollowupStatus)
                         .FirstAsync(a => a.Id == Id);
            return followupStatus;

        }
        public async Task<Int32> UpdateAsync(ApplicantFollowupStatus applicantFollowupStatus)
        {

            _context.ApplicantFollowupStatuses.Update(applicantFollowupStatus);
            var response = await _context.SaveChangesAsync();


            return response;
        }

        public async Task<int> GetApplicantByPassportNumber(string passportNumber)
        {
            return await _context.ApplicantFollowupStatuses.CountAsync(fs => fs.PassportNumber == passportNumber);
        }
    }
}