

using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class Payment
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public Guid? OrderId { get; set; }

    // Navigation properties
    public Order? Order { get; set; }

    [NotMapped]
    public decimal RemainingAmount => TotalAmount - PaidAmount;

    public void AddPayment(decimal amount)
    {
        if (RemainingAmount < amount)
        {
            throw new Exception($"Cannot pay more than the remaining payment ({RemainingAmount}).");
        }
        PaidAmount += amount;
    }

}