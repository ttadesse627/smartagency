using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;


namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
public class CreateApplicantRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PassportNumber { get; set; }
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

    // public Guid? ApplicantPartnerId { get; set; }

    public Guid? ApplicantBrokerNameId { get; set; }
    public Guid? ApplicantBranchId { get; set; }
    public Guid? ApplicantExprienceId { get; set; }
    public Guid? ApplicantJobtitleId { get; set; }
    public Guid? ApplicantReligionId { get; set; }
    public Guid? ApplicantIssuingCountryId { get; set; }
    public Guid? ApplicantIssuedPlaceId { get; set; }
    public Guid? ApplicantHealthId { get; set; }
    public Guid? ApplicantSalaryId { get; set; }
    public Guid? ApplicantDesiredCountryId { get; set; }
    public Guid? ApplicantMaritalStatusId { get; set; }

    public ICollection<WitnessRequest>? ApplicantWitnesses { get; set; }
    public ICollection<BeneficiaryRequest>? ApplicantBeneficiaries { get; set; }
    public ICollection<FileCollectionRequest>? ApplicantFileCollections { get; set; }
    public ICollection<LanguageRequest>? ApplicantLanguages { get; set; }
    public ICollection<ExperienceRequest>? ApplicantExperiences { get; set; }
    public RepersentativeRequest? ApplicantRepersentative { get; set; }
    // public Partner? ApplicantPartner { get; set; }
    public EducationRequest? ApplicantEducation { get; set; }
    public BankAccountRequest? ApplicantBankAccount { get; set; }
    public EmergencyContactRequest? ApplicantEmergencyContact { get; set; }
    public ICollection<Guid>? ApplicantTechnicalSkills { get; set; }
    public AddressRequest? ApplicantAddress { get; set; }
}