using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Partners
{
    public class PartnerAddressRequest
    {
        public Guid? CountryId { get; set; }
        public string? City { get; set; }
       public string? District { get; set; }
        public string? DistrictArabic { get; set; }
        public string? Street { get; set; }
        public string? StreetArabic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
       
    }
}