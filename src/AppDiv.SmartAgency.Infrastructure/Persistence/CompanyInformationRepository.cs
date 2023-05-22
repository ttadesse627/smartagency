

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class CompanyInformationRepository: BaseRepository<CompanyInformation>, ICompanyInformationRepository
{
    private readonly SmartAgencyDbContext _context;
    public CompanyInformationRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    { 
            _context=dbContext;
    }

    public override async Task InsertAsync(CompanyInformation companyInformation, CancellationToken cancellationToken)
    {
        await base.InsertAsync(companyInformation, cancellationToken);
    }
   
   public async Task<Int32> UpdateAsync(CompanyInformation companyInformation)
   {
      
        _context.CompanyInformations.Update(companyInformation);
        var response = await _context.SaveChangesAsync();

        return response;
    }
}
}