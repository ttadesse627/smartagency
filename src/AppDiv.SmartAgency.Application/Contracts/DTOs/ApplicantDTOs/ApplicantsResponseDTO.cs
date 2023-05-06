
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ApplicantsResponseDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string PassportNumber { get; set; }
    public PartnerApplRespDTO Partner { get; set; }
}