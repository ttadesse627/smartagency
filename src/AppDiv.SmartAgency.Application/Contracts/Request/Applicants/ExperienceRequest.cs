

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class ExperienceRequest
{
    public Guid CountryLookupId { get; set; }
    public int PeriodLength { get; set; }
    public string Position { get; set; }
}