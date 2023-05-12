using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class ApplicantFollowupStatusRepository: BaseRepository<ApplicantFollowupStatus>, IApplicantFollowupStatusRepository
{
    private readonly SmartAgencyDbContext _context;
    public ApplicantFollowupStatusRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(ApplicantFollowupStatus applicantFollowupStatus, CancellationToken cancellationToken)
    {
        
        await base.InsertAsync(applicantFollowupStatus, cancellationToken);
    }
    public async Task<ApplicantFollowupStatus> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }
    public async Task<Int32> UpdateAsync(Deposit deposit)
   {
      
        _context.Deposits.Update(deposit);
        var response = await _context.SaveChangesAsync();

        return response;
    }
   
    }
}