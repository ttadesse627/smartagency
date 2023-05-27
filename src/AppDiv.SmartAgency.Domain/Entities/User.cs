using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class User:BaseAuditableEntity
    {
        
         public string FullName { get; set; }
        public string UserName { get; set; }
        public bool ManageAllAppicant { get; set; }=false;
        public Guid? PostionId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PartnerId { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string? BankName { get; set; }
        public string? BankAccount { get; set; }
        public string? HeaderLogo { get; set; }
        public string? ReferenceNumber { get; set; }

        // Foreign Keys
        public Guid? AddressId { get; set; }

        // Navigation properties
        public Address? Address { get; set; }
         public Partner? Partner { get; set; }

         public LookUp?  Branch{ get; set; }
         public LookUp  Postion{ get; set; }
        
    }
}