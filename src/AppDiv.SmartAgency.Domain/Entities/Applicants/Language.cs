

using System.Text.Json.Serialization;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Language
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid LanguageLookUpId { get; set; }
    public LookUp LanguageLookUp { get; set; }
    [JsonPropertyName("LanguageAbility")]
    public LanguageAbility LanguageAbility { get; set; }
    public Guid? ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
}