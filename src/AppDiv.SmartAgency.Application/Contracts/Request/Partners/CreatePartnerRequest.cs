using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Partners
{
    public class CreatePartnerRequest
    {
        public string PartnerType { get; set; }
        public string PartnerName { get; set; } = string.Empty;
        public string PartnerNameAmharic { get; set; }
        public string PartnerNameArabic { get; set; }
        public string ContactPerson { get; set; }
        public string IdNumber { get; set; }
        public string ManagerNameAmharic { get; set; }
        public string LicenseNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string? HeaderLogo { get; set; }
        public string ReferenceNumber { get; set; }
        public PartnerAddressRequest Address { get; set; }
    }
}