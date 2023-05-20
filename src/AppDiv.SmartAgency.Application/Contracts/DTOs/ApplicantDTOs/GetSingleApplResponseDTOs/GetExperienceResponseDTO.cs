

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public class GetExperienceResponseDTO
{
    public int PeriodLength { get; set; }
    public string? Position { get; set; }
    public Guid? CountryId { get; set; }
}