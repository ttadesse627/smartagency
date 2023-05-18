using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("Applicants")]
public class Applicant : PersonalInfo
{
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime IssuedDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string PlaceOfBirth { get; set; } = string.Empty;
    public string AmharicFullName { get; set; } = string.Empty;
    public string? ArabicFullName { get; set; }
    public string? Complexion { get; set; }
    public int NumberOfChildren { get; set; }
    public string? MotherName { get; set; }
    public string? PreviousNationality { get; set; }
    public string? CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int ContractPeriod { get; set; }
    public string? AmharicJobTitle { get; set; }
    public string? Remark { get; set; }
    public bool IsRequested { get; set; } = false;
    public bool IsReserved { get; set; } = false;
    public bool IsBlocked { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    //Foreign keys
    public Guid? IssuingCountryId { get; set; }
    public Guid? PassportIssuedPlaceId { get; set; }
    public Guid? MaritalStatusId { get; set; }
    public Guid? HealthId { get; set; }
    public Guid? ReligionId { get; set; }
    public Guid? JobtitleId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid? LanguageId { get; set; }
    public Guid? SalaryId { get; set; }
    public Guid? DesiredCountryId { get; set; }
    public Guid? BrokerNameId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? PartnerId { get; set; }
    public Guid? AddressId { get; set; }

    // Objects
    public ICollection<LanguageSkill>? LanguageSkills { get; set; }
    public ICollection<LookUp>? Skills { get; set; }
    public ICollection<Experience>? Experiences { get; set; }
    public Education? Education { get; set; }
    public BankAccount? BankAccount { get; set; }
    public EmergencyContact? EmergencyContact { get; set; }
    public Representative? Representative { get; set; }
    public ICollection<Witness>? Witnesses { get; set; }
    public ICollection<Beneficiary>? Beneficiaries { get; set; }
    public ICollection<AttachmentFile>? AttachmentFiles { get; set; }
    public Address? Address { get; set; }

    //Navigation Properties
    public Partner? Partner { get; set; }
    public LookUp? IssuingCountry { get; set; }
    public LookUp? PassportIssuedPlace { get; set; }
    public LookUp? MaritalStatus { get; set; }
    public LookUp? Health { get; set; }
    public LookUp? Religion { get; set; }
    public LookUp? Jobtitle { get; set; }
    public LookUp? Experience { get; set; }
    public LookUp? Language { get; set; }
    public LookUp? Salary { get; set; }
    public LookUp? DesiredCountry { get; set; }
    public LookUp? BrokerName { get; set; }
    public LookUp? Branch { get; set; }
    public Order? Order { get; set; }
    public ICollection<Deposit>? Deposits { get; set; }
    public ICollection<ApplicantFollowupStatus>? ApplicantFollowupStatuses { get; set; }
}