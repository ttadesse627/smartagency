using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class UserGroup : BaseAuditableEntity
    {
        public string GroupName { get; set; }
        public string? DescriptionStr { get; set; }
        public string RolesStr { get; set; }
        [NotMapped]
        public JArray Roles
        {
            get
            {
                return JsonConvert.DeserializeObject<JArray>(string.IsNullOrEmpty(RolesStr) ? "{}" : RolesStr);
            }
            set
            {
                RolesStr = value.ToString();
            }
        }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
