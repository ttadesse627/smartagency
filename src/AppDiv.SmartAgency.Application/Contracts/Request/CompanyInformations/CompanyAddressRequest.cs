using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class CompanyAddressRequest
    {
        public Guid? AddressRegionId { get; set; }
        public string? City { get; set; }
        public string? SubCity { get; set; }
        public string? SubCityArabic { get; set; }
        public string? Woreda { get; set; }
        public string? WoredaArabic { get; set; }
        //primaryphone==PhoneNumber
        public string? PhoneNumber { get; set; }
        public string? OfficePhone { get; set; }
        //Mobile== Mobile2
        public string? Mobile { get; set; }
        //AlternativePhone==Moblie2
         public string? AlternativePhone { get; set; } 
        public string? Fax { get; set; }
        public string? PostCode { get; set; }
        public string? Addres { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
       
    }
}