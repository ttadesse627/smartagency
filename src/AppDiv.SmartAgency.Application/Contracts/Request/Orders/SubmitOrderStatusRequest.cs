
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record SubmitOrderStatusRequest
{
    public StatusInfoRequest? StatusInformation { get; set; }
    public TravelInfoRequest? TravelInformation { get; set; }
}