

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Experience
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
    public Guid? CountryLookupId { get; set; }
    public LookUp? CountryLookup { get; set; }
    public int PeriodLength { get; set; }
    public string? Position { get; set; }
    
}