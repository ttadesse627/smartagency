

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record GetTraveledApplicantResponseDTO
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
    public string VisaNumber { get; set; }
    public string SponsorName { get; set; }
    public string PortOfArrival { get; set; }
}