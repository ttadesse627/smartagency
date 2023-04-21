using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Settings;

namespace AppDiv.SmartAgency.Application.Contracts.Request
{
    public record AddCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddCustomerRequest() => CreatedDate = DateTime.Now;
    }

}
