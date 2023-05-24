

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
public record GetLevelOfQualificationResponseDTO
{
    public LookUpResponseDTO? LookUp { get; set; }
}