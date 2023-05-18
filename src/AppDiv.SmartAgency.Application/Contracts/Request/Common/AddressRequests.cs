
namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public record AddressRequest
{
        public string? City { get; set; }
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
        public Guid? AddressRegionId { get; set; }
}