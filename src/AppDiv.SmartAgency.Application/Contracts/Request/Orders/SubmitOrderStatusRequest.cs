
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record SubmitOrderStatusRequest
{
    public Guid? OrderId { get; set; }
    public StatusInfoRequest? StatusInformation { get; set; }
    public TravelInfoRequest? TravelInformation { get; set; }
}