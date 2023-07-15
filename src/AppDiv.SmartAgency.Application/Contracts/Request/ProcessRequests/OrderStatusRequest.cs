
namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record OrderStatusRequest
{
    public Guid StatusId { get; set; }
    public DateTime Date { get; set; }
}