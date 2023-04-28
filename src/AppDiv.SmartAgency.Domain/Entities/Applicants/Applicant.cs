using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("Applicant")]
public class Applicant : PersonalInfo
{
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
    public string ReligionId { get; set; }
    public LookUp Religion { get; set; }
    public ICollection<LookUp> LookUpJobTitles { get; set; }
    public decimal Salary { get; set; }
    public string DesiredCountry { get; set; }
    public string MotherFullName { get; set; }
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
    public string LanguageId { get; set; }
    public ICollection<Language> Languages { get; set; }
    public ICollection<LookUp> TechnicalSkills { get; set; }
    public ICollection<Experience> Experiences { get; set; }
    public ICollection<Education> ApplicantEducations { get; set; }
    public BankAccount? BankAccount { get; set; }
    public EmergencyContact? EmergencyContact { get; set; }
    public string? RepersentativeId { get; set; }
    public Repersentative? Repersentative { get; set; }
    public ICollection<Witness>? Witnesses { get; set; }
    public ICollection<Beneficiary>? Beneficiaries { get; set; }
    public ICollection<AttachmentFile>? AttachmentFiles { get; set; }
    public string? AddressId { get; set; }
    public Address? Address { get; set; }
    public bool IsOnline { get; set; } = false;
    public bool IsRequested { get; set; } = false;
    public bool IsReserved { get; set; } = false;
    public bool IsBlocked { get; set; } = false;
}