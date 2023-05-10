using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("Applicant")]
public class Applicant : PersonalInfo
{
    public string? PassportNumber { get; set; }
    public string? IssuingCountry { get; set; }
    public DateTime IssuedDate { get; set; }
    public string? IssuedPlace { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string? PlaceOfBirth { get; set; }

    public string? AmharicFullName { get; set; }
    public string? ArabicFullName { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string? Complexion { get; set; }
    public int NumberOfChildren { get; set; }
    public string? Health { get; set; }
    public Guid? ReligionId { get; set; }
    public LookUp? Religion { get; set; }
    public ICollection<AppLookJobtitle>? LookUpJobTitles { get; set; }
    public decimal Salary { get; set; }
    public string? DesiredCountry { get; set; }
    public string? MotherFullName { get; set; }
    public string? PreviousCountry { get; set; }
    public string? CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public int ContractPeriod { get; set; }
    public string? JobTitleAmharic { get; set; }
    public string? BrokerName { get; set; }
    public string? Branch { get; set; }
    public string? Remark { get; set; }
    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }
    public ICollection<Language>? Languages { get; set; }
    public ICollection<TechnicalSkill>? TechnicalSkills { get; set; }
    public ICollection<Experience>? Experiences { get; set; }
    public ICollection<Deposit>? Deposits{ get; set; }
    public Education? ApplicantEducation { get; set; }
    public BankAccount? BankAccount { get; set; }
    public EmergencyContact? EmergencyContact { get; set; }
    public Guid? RepersentativeId { get; set; }
    public Repersentative? Repersentative { get; set; }
    public ICollection<Witness>? Witnesses { get; set; }
    public ICollection<Beneficiary>? Beneficiaries { get; set; }
    public ICollection<AttachmentFile>? AttachmentFiles { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
    
    public bool IsOnline { get; set; } = false;
    public bool IsRequested { get; set; } = false;
    public bool IsReserved { get; set; } = false;
    public bool IsBlocked { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}