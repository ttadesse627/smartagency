using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IDepositRepository : IBaseRepository<Deposit>
    {
        Task<Deposit?> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(Deposit deposit);

        Task<int> GetApplicantPassportByPassportNumber(string passportNumber);

    }
}