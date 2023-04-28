

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class TechnicalSkill
{
    public string ApplicantTechnicalSkillId { get; set; }
    public Applicant ApplicantTechnicalSkill { get; set; }
    public string LookUpTechnicalSkillId { get; set; }
    public LookUp LookUpTechnicalSkill { get; set; }
}