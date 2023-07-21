using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class Setting : BaseAuditableEntity
    {
        public string Key { get; set; }
        public string ValueStr { get; set; }
        [NotMapped]
        public JObject Value
        {

            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(ValueStr) ? "{}" : ValueStr);
            }
            set
            {
                ValueStr = value.ToString();
            }
        }
    }
}