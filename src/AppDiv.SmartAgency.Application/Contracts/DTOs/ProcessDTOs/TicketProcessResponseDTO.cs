

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record TicketProcessResponseDTO
{
    public ICollection<GetTicketReadyApplicantsResponseDTO>? TicketReadyApplicants { get; set; }
    public ICollection<GetTicketRegistrationApplicantsResponseDTO>? TicketRegistrationApplicants { get; set; }
    public ICollection<GetTicketRefundApplicantsResponseDTO>? TicketRefundApplicants { get; set; }
    public ICollection<GetTicketRebookApplicantsResponseDTO>? TicketRebookApplicants { get; set; }
    public ICollection<GetTicketRegistrationApplicantsResponseDTO>? TicketRebookRegistrationApplicants { get; set; }
    public ICollection<GetTraveledApplicantsResponseDTO>? TraveledApplicants { get; set; }
}