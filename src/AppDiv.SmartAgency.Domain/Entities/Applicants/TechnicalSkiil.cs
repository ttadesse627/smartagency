

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class TechnicalSkill
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? ApplicantTechnicalSkillId { get; set; }
    public Applicant? ApplicantTechnicalSkill { get; set; }
    public Guid? LookUpTechnicalSkillId { get; set; }
    public LookUp? LookUpTechnicalSkill { get; set; }
}