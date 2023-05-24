

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record OrderAssignmentRequest
{
    public ICollection<OrderAssignment>? OrderAssignments { get; set; }
}