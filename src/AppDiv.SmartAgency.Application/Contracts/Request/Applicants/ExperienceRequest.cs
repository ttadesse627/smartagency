

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class ExperienceRequest
{
    public string CountryId { get; set; }
    public int PeriodLength { get; set; }
    public string Position { get; set; }
}