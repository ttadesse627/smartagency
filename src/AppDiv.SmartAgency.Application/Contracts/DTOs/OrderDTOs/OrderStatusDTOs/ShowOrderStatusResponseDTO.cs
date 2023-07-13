

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record ShowOrderStatusResponseDTO
{
    public OrderInfoResponseDTO? OrderInformation { get; set; }
    public ApplicantInfoResponseDTO? ApplicantInformation { get; set; }
    public ICollection<StatusInfoResponseDTO>? StatusInformation { get; set; }
     public EnjazResponseDTO? EnjazResponse { get; set; }
    public TravelInfoResponseDTO? TravelInformation { get; set; }
}