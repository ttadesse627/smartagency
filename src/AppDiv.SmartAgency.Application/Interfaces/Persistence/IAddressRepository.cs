using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<Address?> GetByIdAsync(Guid id);
    }
}