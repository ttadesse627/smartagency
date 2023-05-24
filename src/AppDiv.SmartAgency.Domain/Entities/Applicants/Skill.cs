

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Skill
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid LookUpId { get; set; }
    public Applicant? Applicant { get; set; }
    public LookUp? LookUp { get; set; }
}