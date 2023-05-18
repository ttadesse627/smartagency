using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class LanguageSkill
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool CanWrite { get; set; }
    public bool CanRead { get; set; }
    public bool CanSpeak { get; set; }
    public bool CanListen { get; set; }
    public LanguageProficiency Proficiency { get; set; }
    
    //Foreign Keys
    public Guid? LanguageId { get; set; }
    public Guid? ApplicantId { get; set; }

    // Navigation proprties
    public LookUp? Language { get; set; }
    public Applicant? Applicant { get; set; }

}