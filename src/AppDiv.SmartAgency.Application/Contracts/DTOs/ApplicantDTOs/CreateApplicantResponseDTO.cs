

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record CreateApplicantResponseDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
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
    public string LanguageId { get; set; }
    public List<Language> Languages { get; set; }
    public string TechnicalSkillId { get; set; }
    public List<TechnicalSkill> TechnicalSkills { get; set; }
    public string ExperienceId { get; set; }
    public List<Experience> Experiences { get; set; }
    public string EducationId { get; set; }
    public Education Education { get; set; }
    // public string BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; }
    // public string EmergencyContactId { get; set; }
    public EmergencyContact EmergencyContact { get; set; }
    // public string RepersentativeId { get; set; }
    public Repersentative Repersentative { get; set; }
    // public string WitnessId { get; set; }
    public List<Witness> Witnesses { get; set; }
    // public string BeneficiaryId { get; set; }
    public List<Beneficiary> Beneficiaries { get; set; }
    // public string AttachmentFileId { get; set; }
    public List<AttachmentFile> AttachmentFile { get; set; }
    // public string AddressId { get; set; }
    public Address Address { get; set; }
}