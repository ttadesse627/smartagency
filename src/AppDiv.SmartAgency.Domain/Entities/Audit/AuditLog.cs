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
        public string AuditId { get; set; } = Guid.NewGuid().ToString();
        public string AuditData { get; set; }
        public string Action { get; set; }
        public string Enviroment { get; set; }
        public string EntityType { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditUserId { get; set; } = Guid.NewGuid().ToString();
        public string TablePk { get; set; }
    }
}
