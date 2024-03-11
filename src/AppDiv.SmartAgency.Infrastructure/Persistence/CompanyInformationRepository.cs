using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class CompanyInformationRepository(SmartAgencyDbContext dbContext) : BaseRepository<CompanyInformation>(dbContext), ICompanyInformationRepository
    {
        private readonly SmartAgencyDbContext _context = dbContext;

        public async Task<CompanyInformation> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }

        public override async Task InsertAsync(CompanyInformation companyInformation, CancellationToken cancellationToken)
        {
            await base.InsertAsync(companyInformation, cancellationToken);
        }

        public async Task<int> UpdateAsync(CompanyInformation companyInformation)
        {

            _context.CompanyInformations.Update(companyInformation);
            return await _context.SaveChangesAsync();
        }

        public async Task<CompanyInformation> GetByNameAsync(string name)
        {
            ICollection<string> properties = ["Address", "Address.Region", "Witnesses", "CompanySetting", "CountryOperations", "LookUpCountryOperation"];
            var companyInformation = _context.CompanyInformations.AsQueryable();
            foreach (var property in properties)
            {
                companyInformation.Include(property);
            };
            return await companyInformation.FirstAsync();
        }
    }
}