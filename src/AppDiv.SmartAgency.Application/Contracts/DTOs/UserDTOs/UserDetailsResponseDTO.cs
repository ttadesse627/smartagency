using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs
{
    public record UserDetailsResponseDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Position { get; set; }
        public string? Branch { get; set; }
        public UserPartnerResponseDTO? Partner { get; set; }
        public virtual AddressResponseDTO Address { get; set; }
        public ICollection<GroupDTO> UserGroups { get; set; }
    }
}
