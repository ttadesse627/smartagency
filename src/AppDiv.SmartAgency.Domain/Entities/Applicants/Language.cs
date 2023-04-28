

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Language
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string LanguageLookUpId { get; set; }
    public LookUp LanguageLookUp { get; set; }
    public LanguageAbility LanguageAbility { get; set; }
    public List<Applicant> Applicants { get; set; }
}