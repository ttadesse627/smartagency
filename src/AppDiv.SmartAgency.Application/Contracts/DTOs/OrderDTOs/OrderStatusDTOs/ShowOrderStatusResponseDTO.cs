

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record ShowOrderStatusResponseDTO
{
    public OrderInfoResponseDTO? OrderInformation { get; set; }
    public ICollection<ApplicantInfoResponseDTO>? ApplicantInformation { get; set; }
    public StatusInfoResponseDTO? StatusInformation { get; set; }
    public TravelInfoResponseDTO? TravelInformation { get; set; }
}