

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public record GetBeneficiaryResponseDTO
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public float Rate { get; set; }
    public LookUpResponseDTO? Relationship { get; set; }

}