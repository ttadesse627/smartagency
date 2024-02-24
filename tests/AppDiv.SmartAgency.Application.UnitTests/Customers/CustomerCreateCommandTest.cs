using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Infrastructure;
using AppDiv.SmartAgency.Infrastructure.Persistence;
using AppDiv.SmartAgency.Infrastructure.Persistence.Settings;
using AppDiv.SmartAgency.Test.Mock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.UnitTests.Customers
{
    public class CustomerCreateCommandTest : IDisposable
    {
        private readonly SmartAgencyDbContext _Context;
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
