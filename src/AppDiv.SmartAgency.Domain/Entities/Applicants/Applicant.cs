using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("Applicants")]
public class Applicant : PersonalInfo
{
    public string? PassportNumber { get; set; }
    public string? IssuingCountry { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? AmharicFullName { get; set; }
    public string? ArabicFullName { get; set; }
    public string? Complexion { get; set; }
    public int NumberOfChildren { get; set; }
    public string? MotherFullName { get; set; }
    public string? PreviousCountry { get; set; }
    public string? CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int ContractPeriod { get; set; }
    public string? JobTitleAmharic { get; set; }
    public string? Remark { get; set; }
    public bool IsRequested { get; set; } = false;
    public bool IsReserved { get; set; } = false;
    public bool IsBlocked { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    //Foreign keys
    public Guid? ApplicantPartnerId { get; set; }
    public Guid? ApplicantAddressId { get; set; }
    public Guid? ApplicantBankAccountId { get; set; }
    public Guid? ApplicantBrokerNameId { get; set; }
    public Guid? ApplicantBranchId { get; set; }
    public Guid? ApplicantJobtitleId { get; set; }
    public Guid? ApplicantReligionId { get; set; }
    public Guid? ApplicantIssuingCountryId { get; set; }
    public Guid? ApplicantIssuedPlaceId { get; set; }
    public Guid? ApplicantHealthId { get; set; }
    public Guid? ApplicantSalaryId { get; set; }
    public Guid? ApplicantDesiredCountryId { get; set; }
    public Guid? ApplicantMaritalStatusId { get; set; }

    //Navigation Properties
    public ICollection<Witness>? ApplicantWitnesses { get; set; }
    public ICollection<Beneficiary>? ApplicantBeneficiaries { get; set; }
    public ICollection<FileCollection>? ApplicantFileCollections { get; set; }
    public ICollection<Language>? ApplicantLanguages { get; set; }
    public ICollection<Experience>? ApplicantExperiences { get; set; }
    public Repersentative? ApplicantRepersentative { get; set; }
    public Partner? ApplicantPartner { get; set; }
    public Address? ApplicantAddress { get; set; }
    public Education? ApplicantEducation { get; set; }
    public BankAccount? ApplicantBankAccount { get; set; }
    public EmergencyContact? ApplicantEmergencyContact { get; set; }
    public ICollection<LookUp>? ApplicantTechnicalSkills { get; set; }
    public LookUp? ApplicantReligion { get; set; }
    public LookUp? ApplicantBrokerName { get; set; }
    public LookUp? ApplicantBranch { get; set; }
    public LookUp? ApplicantJobtitle { get; set; }
    public LookUp? ApplicantIssuingCountry { get; set; }
    public LookUp? ApplicantIssuedPlace { get; set; }
    public LookUp? ApplicantHealth { get; set; }
    public LookUp? ApplicantSalary { get; set; }
    public LookUp? ApplicantDesiredCountry { get; set; }
    public LookUp? ApplicantMaritalStatus { get; set; }
    public Order? ApplicantOrder { get; set; }
    public ICollection<Deposit>? Deposits{ get; set; }
    public ICollection<ApplicantFollowupStatus>? ApplicantFollowupStatuses{ get; set; }
}