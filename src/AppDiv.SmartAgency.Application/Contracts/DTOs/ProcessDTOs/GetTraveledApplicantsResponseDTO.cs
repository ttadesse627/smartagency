namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetTraveledApplicantsResponseDTO
{
    public Guid Id { get; set; }
    public string PassportNumber { get; set; }
    public string? FullName { get; set; }
    public DateTime? Date { get; set; }
}