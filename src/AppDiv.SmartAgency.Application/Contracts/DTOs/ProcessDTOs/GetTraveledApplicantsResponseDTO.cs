namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetTraveledApplicantsResponseDTO : GetTicketRegistrationApplicantsResponseDTO
{
    public string? FullName { get; set; }
    public DateTime? Date { get; set; }
}