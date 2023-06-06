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
        public Dictionary<string, List<string>> Description
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(string.IsNullOrEmpty(DescriptionStr) ? "{}" : DescriptionStr);
            }
            set
            {
                DescriptionStr = JsonConvert.SerializeObject(value);
            }
        }
        [NotMapped]
        public List<string> Roles
        {
            get
            {
                return JsonConvert.DeserializeObject<List<string>>(string.IsNullOrEmpty(RolesStr) ? "[]" : RolesStr);
            }
            set
            {
                RolesStr = JsonConvert.SerializeObject(value);
            }
        }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
