namespace AppDiv.SmartAgency.Application.Contracts.DTOs.DeletedInfoDTOs;
public record DeletedOrderApplResponseDTO
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
}