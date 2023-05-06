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
    public string? Health { get; set; }
    public Guid ReligionId { get; set; }
    public ICollection<Guid>? LookUpJobtitleId { get; set; }
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
    // public Guid? PartnerId { get; set; }
    public ICollection<LanguageRequest> Languages { get; set; }
    public ICollection<TechnicalSkillRequest> TechnicalSkills { get; set; }
    public ICollection<ExperienceRequest> Experiences { get; set; }
    public EducationRequest Education { get; set; }
    public BankAccountRequest BankAccount { get; set; }
    public EmergencyContactRequest EmergencyContact { get; set; }
    public RepersentativeRequest Repersentative { get; set; }
    public ICollection<WitnessRequest> Witnesses { get; set; }
    public ICollection<BeneficiaryRequest> Beneficiaries { get; set; }
    public AddressRequest Address { get; set; }
}