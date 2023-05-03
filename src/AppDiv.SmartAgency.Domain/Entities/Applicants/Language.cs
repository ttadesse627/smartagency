using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Language
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
    public Guid? LanguageLookUpId { get; set; }
    public LookUp? LanguageLookUp { get; set; }
    public string? AbilityJson { get; set; }
    [NotMapped]
    public LanguageAbility? Ability {get; set; }
    public void SetAbilityJson()
    {
        AbilityJson = JsonSerializer.Serialize(Ability, new JsonSerializerOptions { IgnoreNullValues = true });
    }

    public void SetAbilityObject()
    {
        Ability = JsonSerializer.Deserialize<LanguageAbility>(AbilityJson);
    }

}