using AppDiv.SmartAgency.Domain.Entities.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class UserGroup : BaseAuditableEntity
    {
        public string GroupName { get; set; } = null!;
        public string? DescriptionStr { get; set; }
        public string RolesStr { get; set; } = null!;
        [NotMapped]
        public JArray Roles
        {
            get
            {
                return JsonConvert.DeserializeObject<JArray>(string.IsNullOrEmpty(RolesStr) ? "{}" : RolesStr)!;
            }
            set
            {
                RolesStr = value.ToString();
            }
        }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = Array.Empty<ApplicationUser>();
    }
}
