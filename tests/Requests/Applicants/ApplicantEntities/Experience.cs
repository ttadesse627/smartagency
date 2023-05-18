

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Experience
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int PeriodLength { get; set; }
    public string? Position { get; set; }

    // Foreign Keys
    public Guid? ExperienceApplicantId { get; set; }
    public Guid? ExperienceCountryId { get; set; }

    // Navigation properties
    public Applicant? ExperienceApplicant { get; set; }
    public LookUp? ExperienceCountry { get; set; }
    
}