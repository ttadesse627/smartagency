using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Language
{
    public Guid Id { get; set; } = Guid.NewGuid();

    //Foreign Keys
    public Guid? LanguageApplicantId { get; set; }
    public Guid? LanguageLookUpId { get; set; }

    // Additional properties
    public bool CanWrite { get; set; }
    public bool CanRead { get; set; }
    public bool CanSpeak { get; set; }
    public bool CanListen { get; set; }
    public LanguageProficiency Proficiency { get; set; }

    // Navigation proprties
    public Applicant? LanguageApplicant { get; set; }
    public LookUp? LanguageLookUp { get; set; }

}