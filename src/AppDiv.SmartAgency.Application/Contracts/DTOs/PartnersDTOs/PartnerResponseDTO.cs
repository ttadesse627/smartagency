
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class PartnerResponseDTO
    {

        public Guid Id { get; set; }
        public string PartnerType { get; set; }
        public string PartnerName { get; set; }
        public string PartnerNameAmharic { get; set; }
        public string PartnerNameArabic { get; set; }
        public string ContactPerson { get; set; }
        public string IdNumber { get; set; }
        public string ManagerNameAmharic { get; set; }
        public string LicenseNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string HeaderLogo { get; set; }
        public string ReferenceNumber { get; set; }
        public Guid AddressId {get; set;}
        public PartnerAddressResponseDTO Address { get; set; }
    }
}