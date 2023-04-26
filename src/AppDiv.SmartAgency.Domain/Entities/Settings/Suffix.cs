using AppDiv.SmartAgency.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities.Settings
{
    public class Suffix : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;

    }
}
