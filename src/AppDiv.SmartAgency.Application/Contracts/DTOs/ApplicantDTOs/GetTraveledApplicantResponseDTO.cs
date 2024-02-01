

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record GetTraveledApplicantResponseDTO
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string PassportNumber { get; set; }
    public required string VisaNumber { get; set; }
    public required string SponsorName { get; set; }
    public required string PortOfArrival { get; set; }
}