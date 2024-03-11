

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs
{
    public class UserGroupResponseDTO
    {
        public ICollection<DropDownDto> UserGroups { get; set; } = [];
    }
}