

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Language
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public LookUp LookUp { get; set; }
    public LanguageAbility Ability { get; set; }
    public string ApplicantId { get; set; }
    public List<Applicant> Applicants { get; set; }
}