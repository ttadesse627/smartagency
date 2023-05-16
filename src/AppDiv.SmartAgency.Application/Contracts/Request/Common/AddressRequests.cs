
namespace AppDiv.SmartAgency.Application.Contracts.Request.Common;
public class AddressRequest
{
        public string? Region { get; set; }
        public string? Zone { get; set; }
        public string? Woreda { get; set; }
        public string? Kebele { get; set; }
        public string? PhoneNumber { get; set; }
        public string Addres { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public Guid? AddressRegionId { get; set; }
}