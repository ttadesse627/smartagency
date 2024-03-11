using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs
{
    public class CompanyAddressResponseDTO
    {
        // public Guid Id { get; set; }
        public LookUpItemResponseDTO? Region { get; set; }
        public string? SubCity { get; set; }
        public string? Zone { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HouseNumber { get; set; }
        public string? OfficePhone { get; set; }
        public string? Mobile { get; set; }
        public string? AlternativePhone { get; set; }
        public string? Fax { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string? PostCode { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
    }
}