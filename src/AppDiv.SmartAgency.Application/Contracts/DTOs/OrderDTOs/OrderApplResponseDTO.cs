
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record OrderApplResponseDTO
{
    public string? PassportNumber { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
}