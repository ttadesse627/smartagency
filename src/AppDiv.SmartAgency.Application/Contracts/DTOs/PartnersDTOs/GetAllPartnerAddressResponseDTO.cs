using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class GetAllPartnerAddressResponseDTO
    {
          public string? Email { get; set; }
          public string? Mobile { get; set; }
          public string? PhoneNumber { get; set; }
          public  OnlineApplicantLookUpResponseDTO? Country{get; set;}



    }
}