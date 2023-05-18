

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Experience
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int PeriodLength { get; set; }
    public string? Position { get; set; }

    // Foreign Keys
    public Guid? CountryId { get; set; }
    public Guid? ApplicantId { get; set; }

    // Navigation properties
    public LookUp? Country { get; set; }
    public Applicant? Applicant { get; set; }
    
    
}