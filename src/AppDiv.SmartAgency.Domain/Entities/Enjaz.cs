
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Domain.Entities.Base;
namespace AppDiv.SmartAgency.Domain.Entities;
public class Enjaz : BaseAuditableEntity
{
    public string ApplicationNumber { get; set; }
    public int TransactionCode { get; set; }
    public Guid? SponsorId { get; set; }
    public Sponsor? Sponsor { get; set; }

}