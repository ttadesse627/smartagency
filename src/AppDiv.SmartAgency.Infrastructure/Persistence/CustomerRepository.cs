using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await base.GetAsync(id);
        }

        public Task<Customer> GetCustomerByEmail(string email)
        {
            return base.GetFirstEntryAsync(x => x.Email.Equals(email), q => q.Id, Utility.Contracts.SortingDirection.Ascending);
        }
        public override async Task InsertAsync(Customer user, CancellationToken cancellationToken)
        {
            await base.InsertAsync(user, cancellationToken);
        }

    }
}