

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
public record GetSponsorResponseDTO
{
    public string? IdNumber { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAmharic { get; set; }
    public string? FullNameArabic { get; set; }
    public string? OtherName { get; set; }
}