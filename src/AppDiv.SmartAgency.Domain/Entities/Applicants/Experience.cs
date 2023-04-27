

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Experience
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public LookUp CountryLookup { get; set; }
    public int PeriodLength { get; set; }
    public string Position { get; set; }
    public string ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
}