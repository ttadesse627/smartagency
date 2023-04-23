
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
// Interface for CustomerQueryRepository
public interface ICustomerRepository : IBaseRepository<Customer>
{
    //Custom operation which is not generic
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(string id);
    Task<Customer> GetCustomerByEmail(string email);
}
