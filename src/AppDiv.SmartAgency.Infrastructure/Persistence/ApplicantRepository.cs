using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
{
    private readonly SmartAgencyDbContext _context;
    public ApplicantRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {
         _context=dbContext;
    }
    public override async Task InsertAsync(Applicant applicant, CancellationToken cancellationToken)
    {
        await base.InsertAsync(applicant, cancellationToken);
    }

    public async Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber)
        {
            var serviceResponse = new ServiceResponse<Applicant>();
            serviceResponse.Data = _context.Applicants.FirstOrDefault(a=>a.PassportNumber==passportNumber);

          return serviceResponse;

            //return base.GetFirstEntryAsync(x => x.PassportNumber.Equals(passportNumber), q => q.Id, Utility.Contracts.SortingDirection.Ascending);
        }
}