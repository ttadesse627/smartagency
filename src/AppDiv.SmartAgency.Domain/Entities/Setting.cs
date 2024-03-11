using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class Setting : BaseAuditableEntity
    {
        public string Key { get; set; } = string.Empty;
        public string ValueStr { get; set; } = string.Empty;
        [NotMapped]
        public JObject Value
        {
            get => JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(ValueStr) ? "{}" : ValueStr)!;
            set
            {
                ValueStr = value.ToString();
            }
        }
    }
}