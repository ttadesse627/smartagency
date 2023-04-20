
using AppDiv.CRVS.Domain.Base;

namespace AppDiv.CRVS.Domain.Entities{
    public class Lookup : BaseAuditableEntity{
        public string Key { get ; set; }
        public string valueStr { get; set; }
        public string descriptionStr {get; set; }
        public string StatisticCode { get; set; }
        public string Code { get; set; }
        
    }
}