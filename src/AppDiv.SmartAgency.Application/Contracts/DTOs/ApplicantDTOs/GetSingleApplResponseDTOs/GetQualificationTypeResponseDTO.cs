

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
public record GetQualificationTypeResponseDTO
{
    public LookUpResponseDTO? LookUp { get; set; }
}