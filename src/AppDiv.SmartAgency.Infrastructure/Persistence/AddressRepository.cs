using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class AddressRepository(SmartAgencyDbContext dbContext) : BaseRepository<Address>(dbContext), IAddressRepository
    {
        public async Task<Address?> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }
    }
}