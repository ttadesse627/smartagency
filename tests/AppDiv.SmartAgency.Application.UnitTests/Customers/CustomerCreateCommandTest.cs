using AppDiv.CRVS.Domain.Entities.Settings;
using AppDiv.CRVS.Infrastructure;
using AppDiv.CRVS.Infrastructure.Persistence;
using AppDiv.CRVS.Infrastructure.Persistence.Settings;
using AppDiv.CRVS.Test.Mock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.UnitTests.Customers
{
    public class CustomerCreateCommandTest : IDisposable
    {
        private readonly CRVSDbContext _Context;
        private readonly MockDatabase _mockDatabase;
        private CustomerRepository _customerRepository;
        private GenderRepository _genderRepository;
        private SuffixRepository _suffixRepository;
        public CustomerCreateCommandTest()
        {
                
        }
        public void Dispose()
        {
            _Context.Database.EnsureDeleted();
        }
    }
}
