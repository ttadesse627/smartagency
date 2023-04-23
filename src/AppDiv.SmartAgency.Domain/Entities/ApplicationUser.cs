
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserGroupId { get; set; }
        public string PersonalInfoId { get;set;}
        public virtual PersonalInfo PersonalInfo { get; set; }
    }
}
