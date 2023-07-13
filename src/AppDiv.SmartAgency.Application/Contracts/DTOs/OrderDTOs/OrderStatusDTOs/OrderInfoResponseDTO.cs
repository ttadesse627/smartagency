

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record OrderInfoResponseDTO
{
    public string? OrderNumber { get; set; }
    public string? ClientName { get; set; }
    public string? Priority { get; set; }
    public string? VisaNumber { get; set; }
    public string? Sponsor { get; set; }
    public string? City { get; set; }
}