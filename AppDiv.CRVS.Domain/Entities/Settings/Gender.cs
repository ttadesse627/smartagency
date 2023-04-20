using AppDiv.CRVS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.CRVS.Domain.Entities.Settings
{
    public class Gender: BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;        
    }
}
