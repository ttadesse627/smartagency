
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetTicketReadyApplicantsResponseDTO : GetApplProcessResponseDTO
{
    public string? Country { get; set; }
    public string? PortOfArrival { get; set; }

}