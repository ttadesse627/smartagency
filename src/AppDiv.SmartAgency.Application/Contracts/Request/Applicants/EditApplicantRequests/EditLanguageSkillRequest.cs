using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants. EditApplicantRequests;
public class EditLanguageSkillRequest
{
    public Guid Id { get; set; }
    public bool CanWrite { get; set; }
    public bool CanRead { get; set; }
    public bool CanSpeak { get; set; }
    public bool CanListen { get; set; }
    public LanguageProficiency Proficiency { get; set; }
    public Guid? LanguageId { get; set; }
}