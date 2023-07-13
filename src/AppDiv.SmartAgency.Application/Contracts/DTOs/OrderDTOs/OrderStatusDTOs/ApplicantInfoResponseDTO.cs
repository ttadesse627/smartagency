

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record ApplicantInfoResponseDTO
{
    public string? PassportNumber { get; set; }
    public string? FullName { get; set; }
    public string? Sex { get; set; }
    public string? MaritalSatus { get; set; }
    public string? Religion { get; set; }
    public string? DateOfBirth { get; set; }
    public string? CurrentNationality { get; set; }
}