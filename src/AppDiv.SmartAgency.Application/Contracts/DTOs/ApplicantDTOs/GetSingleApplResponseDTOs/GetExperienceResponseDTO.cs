

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public class GetExperienceResponseDTO
{
    public Guid Id { get; set; }
    public int PeriodLength { get; set; }
    public string? Position { get; set; }
    public LookUpResponseDTO? Country { get; set; }
}