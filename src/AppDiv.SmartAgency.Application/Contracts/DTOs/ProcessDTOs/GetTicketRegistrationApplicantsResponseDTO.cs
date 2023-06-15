

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetTicketRegistrationApplicantsResponseDTO
{
    public Guid Id { get; set; }
    public string PassportNumber { get; set; }

}