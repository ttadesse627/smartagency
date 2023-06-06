using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface ICompanyInformationRepository: IBaseRepository<CompanyInformation>
    {
        Task<CompanyInformation> GetByIdAsync(Guid id);
        Task<Int32> UpdateAsync(CompanyInformation companyInformation);
        Task<CompanyInformation> GetByNameAsync(string name);
    }
}