using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IPartnerRepository : IBaseRepository<Partner>
    {
        Task<Partner?> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(Partner partner);
    }
}