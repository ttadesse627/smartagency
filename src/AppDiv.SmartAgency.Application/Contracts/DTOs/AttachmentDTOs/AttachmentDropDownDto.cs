using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs
{
    public class AttachmentDropDownDto

    {
        public Guid Key { get; set; }
        public string? Value { get; set; }
        public bool IsRequired { get; set; }
    }
}