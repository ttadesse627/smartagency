using Audit.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities.Audit
{
    [AuditIgnore]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AuditId { get; set; } = Guid.NewGuid();
        public string AuditData { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Enviroment { get; set; } = null!;
        public string EntityType { get; set; } = null!;
        public DateTime AuditDate { get; set; }
        public Guid AuditUserId { get; set; } = Guid.NewGuid();
        public string TablePk { get; set; } = null!;
    }
}
