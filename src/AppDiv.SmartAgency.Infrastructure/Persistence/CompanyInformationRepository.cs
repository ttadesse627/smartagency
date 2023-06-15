

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class CompanyInformationRepository : BaseRepository<CompanyInformation>, ICompanyInformationRepository
    {
        private readonly SmartAgencyDbContext _context;
        public CompanyInformationRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public async Task<CompanyInformation> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }

        public override async Task InsertAsync(CompanyInformation companyInformation, CancellationToken cancellationToken)
        {
            await base.InsertAsync(companyInformation, cancellationToken);
        }

        public async Task<Int32> UpdateAsync(CompanyInformation companyInformation)
        {

            _context.CompanyInformations.Update(companyInformation);
            return await _context.SaveChangesAsync();


        }

        public async Task<CompanyInformation> GetByNameAsync(string name)
        {

            var companyInformation = await _context.CompanyInformations
                .Include(ci => ci.Address)
                .Include(ci => ci.Address.Region)
                .Include(ci => ci.Witnesses)
                .Include(ci => ci.CompanySetting)
                .Include(ci => ci.CountryOperations)
                .ThenInclude(co => co.LookUpCountryOperation)
                .FirstOrDefaultAsync(ci => ci.CompanyName == name);
            return companyInformation;

        }
    }
}