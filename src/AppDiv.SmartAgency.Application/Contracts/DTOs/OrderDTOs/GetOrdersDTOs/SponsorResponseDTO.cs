
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
public record SponsorResponseDTO
{
    public string IdNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}