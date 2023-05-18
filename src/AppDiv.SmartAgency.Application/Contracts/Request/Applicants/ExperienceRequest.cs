

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class ExperienceRequest
{
    public int PeriodLength { get; set; }
    public string? Position { get; set; }
    public Guid? CountryId { get; set; }
}