

using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Applicant : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string PassportNumber { get; set; }
    public string IssuingCountry { get; set; }
    public DateTime IssuedDate { get; set; }
    public string IssuedPlace { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string PlaceOfBirth { get; set; }
    public string AmharicFullName { get; set; }
    public string ArabicFullName { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string Complexion { get; set; }
    public int NumberOfChildren { get; set; }
    public string Health { get; set; }
    public string LookupId { get; set; }
    public LookUp Religion { get; set; }
    public LookUp JobTitle { get; set; }
    public decimal Salary { get; set; }
    public string DesiredCountry { get; set; }
    public PersonalName MotherName { get; set; }
    public string PreviousCountry { get; set; }
    public string CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public int ContractPeriod { get; set; }
    public string? JobTitleAmharic { get; set; }
    public string? BrokerName { get; set; }
    public string? Branch { get; set; }
    public string? Remark { get; set; }
    public string PartnerId { get; set; }
    public Partner Partner { get; set; }
    public List<Language> Languages { get; set; }
    public List<TechnicalSkill> TechnicalSkills { get; set; }
    public List<Experience> Experiences { get; set; }
    public Education Education { get; set; }
}