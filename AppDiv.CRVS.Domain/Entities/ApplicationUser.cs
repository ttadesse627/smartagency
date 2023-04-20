
using AppDiv.CRVS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.CRVS.Domain
{
    public class ApplicationUser : IdentityUser
    {

        public string UserGroupId { get; set; }
        public string PersonalInfoId { get;set;}
        public virtual PersonalInfo PersonalInfo { get; set; }
    }
}
