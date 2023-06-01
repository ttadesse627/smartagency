
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs
{
    public record UserResponseDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        // public AddressResponseDTO? Address { get; set; }
        public ICollection<GroupDTO> UserGroups { get; set; }

    }
}
