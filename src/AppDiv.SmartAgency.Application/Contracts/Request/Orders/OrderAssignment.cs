

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record OrderAssignment
{
    public Guid OrderId { get; set; }
    public Guid EmployeeId { get; set; }
}