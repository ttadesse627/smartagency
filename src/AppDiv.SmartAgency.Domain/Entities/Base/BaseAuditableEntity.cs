using System.ComponentModel.DataAnnotations;

namespace AppDiv.SmartAgency.Domain.Entities.Base
{
    public abstract class BaseAuditableEntity
    {
        [Key]
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual string? ModifiedBy { get; set; }
        public BaseAuditableEntity() => CreatedAt = DateTime.Now;
    }
}
