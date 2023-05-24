

using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class Payment
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public Guid? OrderId { get; set; }

    // Navigation properties
    public Order? Order { get; set; }
    public void UpdatePayment(decimal currentPaidAmount)
    {
        if (PaidAmount < TotalAmount)
        {
            PaidAmount += currentPaidAmount;
        }
        else throw new Exception($"Cannot pay more than the total payment specified firstly.");
    }

}