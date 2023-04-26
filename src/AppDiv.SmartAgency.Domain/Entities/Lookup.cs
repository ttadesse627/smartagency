
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {

        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string Value { get; set; } = string.Empty;


        //  public string Key { get ; set; }
        // public string Value { get; set; }
        // public string Description {get; set; }
        //public string StatisticCode { get; set; }
        //public string Code { get; set; }

    }
}