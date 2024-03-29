using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;


namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants. EditApplicantRequests;
public record EditApplicantRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
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
    public ICollection<EditLanguageSkillRequest>? LanguageSkills { get; set; }
    public ICollection<Guid>? Skills { get; set; }
    public ICollection<EditExperienceRequest>? Experiences { get; set; }
    public EditEducationRequest? Education { get; set; }
    public EditBankAccountRequest? BankAccount { get; set; }
    public EditEmergencyContactRequest? EmergencyContact { get; set; }
    public EditRepresentativeRequest? Representative { get; set; }
    public ICollection<EditWitnessRequest>? Witnesses { get; set; }
    public ICollection<EditBeneficiaryRequest>? Beneficiaries { get; set; }
    public ICollection<EditAttachmentFileRequest>? AttachmentFiles { get; set; }
    public EditAddressRequest? Address { get; set; }
}