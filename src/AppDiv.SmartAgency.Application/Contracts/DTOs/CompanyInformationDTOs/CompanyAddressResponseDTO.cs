using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs
{
    public class CompanyAddressResponseDTO
    {
        public Guid? Id { get; set; }
        public string? City { get; set; }
        public string? SubCity { get; set; }
        public string? SubCityArabic { get; set; }
        public string? Woreda { get; set; }
        public string? WoredaArabic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OfficePhone { get; set; }
        //Mobile== Mobile2
        public string? Mobile { get; set; }
        //AlternativePhone==Moblie2
         public string? AlternativePhone { get; set; } 
        public string? Fax { get; set; }
        public string? PostCode { get; set; }
        public string?  Adress { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public OnlineApplicantLookUpResponseDTO AddressRegion  { get; set; }
    }
}