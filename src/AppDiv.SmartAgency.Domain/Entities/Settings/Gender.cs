using AppDiv.SmartAgency.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities.Settings
{
    public class Gender: BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;        
    }
}
