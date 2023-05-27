

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public record GetApplSearchRequest
{
    public Guid? JobtitleId { get; set; }
    public Guid? MaritalStatusId { get; set; }
    public int AgeFrom { get; set; }
    public int AgeTo { get; set; }
    public Guid? ReligionId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid? CountryId { get; set; }
}