using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class GetAllPartnerResponseDTO
    {
        public string PartnerName { get; set; }
        public string PartnerType { get; set; }
        public string ContactPerson { get; set; }
        public string LicenseNumber { get; set; }

        public GetAllPartnerAddressResponseDTO? Address { get; set; }
    }
}