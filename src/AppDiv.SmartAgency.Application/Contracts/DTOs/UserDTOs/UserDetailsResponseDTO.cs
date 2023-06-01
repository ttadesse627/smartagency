using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs
{
    public record UserDetailsResponseDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
    }
}
