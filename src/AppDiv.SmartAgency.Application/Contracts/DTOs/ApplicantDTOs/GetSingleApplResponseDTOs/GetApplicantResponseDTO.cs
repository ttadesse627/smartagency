using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs. GetSingleApplResponseDTOs;
public record GetApplicantResponseDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string PassportNumber { get; set; } = string.Empty;
    public string? IssuedDate { get; set; }
    public string? PassportExpiryDate { get; set; }
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
    // public bool IsRequested { get; set; } = false;
    // public bool IsReserved { get; set; } = false;
    // public bool IsBlocked { get; set; } = false;

    // Objects
    public ICollection<GetLanguageSkillResponseDTO>? LanguageSkills { get; set; }
    public ICollection<GetSkillResponseDTO>? Skills { get; set; }
    public ICollection<GetExperienceResponseDTO>? Experiences { get; set; }
    public GetEducationResponseDTO? Education { get; set; }
    public GetBankAccountResponseDTO? BankAccount { get; set; }
    public GetEmergencyContactResponseDTO? EmergencyContact { get; set; }
    public GetRepresentativeResponseDTO? Representative { get; set; }
    public ICollection<GetWitnessResponseDTO>? Witnesses { get; set; }
    public ICollection<GetBeneficiaryResponseDTO>? Beneficiaries { get; set; }
    public ICollection<AttachmentFileResponseDTO>? AttachmentFiles { get; set; }
    public GetAddressResponseDTO? Address { get; set; }

    //Navigation Properties
    public PartnerApplRespDTO? Partner { get; set; }
    public LookUpResponseDTO? IssuingCountry { get; set; }
    public LookUpResponseDTO? PassportIssuedPlace { get; set; }
    public LookUpResponseDTO? MaritalStatus { get; set; }
    public LookUpResponseDTO? Health { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? Jobtitle { get; set; }
    public LookUpResponseDTO? Experience { get; set; }
    public LookUpResponseDTO? Language { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
    public LookUpResponseDTO? DesiredCountry { get; set; }
    public LookUpResponseDTO? BrokerName { get; set; }
    public LookUpResponseDTO? Branch { get; set; }
    //public LookUpResponseDTO? Order { get; set; }
}