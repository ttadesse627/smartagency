
using AppDiv.SmartAgency.Domain.Base;

namespace AppDiv.SmartAgency.Domain.Entities{
    public class Lookup : BaseAuditableEntity{
        public string Key { get ; set; }
        public string Value { get; set; }
        public string Description {get; set; }
        public string StatisticCode { get; set; }
        public string Code { get; set; }
        
    }
}