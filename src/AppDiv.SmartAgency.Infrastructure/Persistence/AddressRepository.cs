using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(SmartAgencyDbContext dbContext) : base(dbContext) { }


        public async Task<Address> GetByIdAsync(Guid Id)
        {
            return await base.GetAsync(Id);
        }
    }
}