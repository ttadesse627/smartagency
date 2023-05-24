

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record UnassignOrderRequest
{
    public ICollection<Guid>? OrderIds { get; set; }
}