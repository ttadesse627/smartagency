
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ExperienceResponseDTO
{
    public Guid Id { get; set; }
    public ApplicantsLookUpResponseDTO? ExperienceCountry { get; set; }
    public int PeriodLength { get; set; }
    public string? Position { get; set; }

}