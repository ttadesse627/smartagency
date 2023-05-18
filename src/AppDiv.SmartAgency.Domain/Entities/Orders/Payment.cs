

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class Payment
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal CurrentPaidAmount { get; set; }
    public Guid? OrderId { get; set; }

    // Navigation properties
    public Order? Order { get; set; }

}