
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Domain.Entities.Base;
namespace AppDiv.SmartAgency.Domain.Entities;
public class Enjaz : BaseAuditableEntity
{
    public string? ApplicationNumber { get; set; }
    public int TransactionCode { get; set; }
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }

}