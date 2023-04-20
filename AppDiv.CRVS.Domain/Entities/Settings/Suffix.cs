using AppDiv.CRVS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.CRVS.Domain.Entities.Settings
{
    public class Suffix : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;

    }
}
