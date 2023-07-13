

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
public record GetApplProcessResponseDTO
{
    public Guid ApplicantId { get; set; }
    public string PassportNumber { get; set; }
    public string FullName { get; set; }
    public string OrderNumber { get; set; }
    public string SponsorName { get; set; }

}