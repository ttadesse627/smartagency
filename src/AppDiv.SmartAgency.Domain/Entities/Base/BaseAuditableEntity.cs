using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Domain.Entities.Base
{
    // Base entity or auditable entity
    public abstract class BaseAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual string? ModifiedBy { get; set; }
        public BaseAuditableEntity() => CreatedAt = DateTime.Now;
    }
}
