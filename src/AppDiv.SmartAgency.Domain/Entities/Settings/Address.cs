using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Settings
{
    public class Address : BaseAuditableEntity
    {
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? Zone { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
    }
}