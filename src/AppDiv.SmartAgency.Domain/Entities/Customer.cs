using AppDiv.SmartAgency.Domain.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Domain.Entities
{
    // Customer entity 
    public class Customer : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? SuffixId { get; set; }
        public Gender? Gender { get; set; } 
        public Suffix? Suffix { get; set; }
    }
}
