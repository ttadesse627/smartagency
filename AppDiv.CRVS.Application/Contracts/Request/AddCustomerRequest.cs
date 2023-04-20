using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Contracts.Request
{
      public record AddCustomerRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string ContactNumber { get; set; }
            public string Address { get; set; }
            public DateTime CreatedDate { get; set; }

            public AddCustomerRequest()
            {
                CreatedDate = DateTime.Now;
            }
        }
   
}
