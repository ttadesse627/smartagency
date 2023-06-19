

using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Complaint : BaseAuditableEntity
{
    public string Message { get; set; }
    public Guid OrderId { get; set; }
    public bool IsClosed { get; set; } = false;
    public ApplicationUser User { get; set; }
    public Order Order { get; set; }
}