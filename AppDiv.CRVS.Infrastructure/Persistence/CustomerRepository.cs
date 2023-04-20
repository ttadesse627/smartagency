using AppDiv.CRVS.Domain.Entities;
using AppDiv.CRVS.Domain.Repositories;
using AppDiv.CRVS.Infrastructure.Context;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Infrastructure.Persistence
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CRVSDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
           return await base.GetAsync(id);
        }

        public Task<Customer> GetCustomerByEmail(string email)
        {
            return base.GetFirstEntryAsync(x=>x.Email.Equals(email), q => q.Id, Utility.Contracts.SortingDirection.Ascending);
        }
        public override async Task InsertAsync(Customer user, CancellationToken cancellationToken)
        {
            await base.InsertAsync(user, cancellationToken);
        }

    }
}