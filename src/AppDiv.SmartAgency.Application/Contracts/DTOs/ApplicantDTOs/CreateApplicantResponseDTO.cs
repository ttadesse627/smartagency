using AppDiv.SmartAgency.Application.Contracts.DTOs.AddressDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record CreateApplicantResponseDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PassportNumber { get; set; }
    public ApplicantsLookUpResponseDTO? IssuingCountry { get; set; }
    public DateTime IssuedDate { get; set; }
    public ApplicantsLookUpResponseDTO? IssuedPlace { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? AmharicFullName { get; set; }
    public string? ArabicFullName { get; set; }
    public ApplicantsLookUpResponseDTO? MaritalStatus { get; set; }
    public string? Complexion { get; set; }
    public int NumberOfChildren { get; set; }
    public ApplicantsLookUpResponseDTO? Health { get; set; }
    public ApplicantsLookUpResponseDTO? Religion { get; set; }
    public ICollection<ApplicantsLookUpResponseDTO>? JobTitles { get; set; }
    public ApplicantsLookUpResponseDTO? Salary { get; set; }
    public ApplicantsLookUpResponseDTO? DesiredCountry { get; set; }
    public string? MotherFullName { get; set; }
    public string? PreviousCountry { get; set; }
    public string? CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int ContractPeriod { get; set; }
    public string? JobTitleAmharic { get; set; }
    public ApplicantsLookUpResponseDTO? BrokerName { get; set; }
    public ApplicantsLookUpResponseDTO? Branch { get; set; }
    public string? Remark { get; set; }
    public PartnerApplRespDTO? Partner { get; set; }
    public ICollection<LanguageResponseDTO>? Languages { get; set; }
    public ICollection<ApplicantsLookUpResponseDTO>? TechnicalSkills { get; set; }
    public ICollection<ExperienceResponseDTO>? Experiences { get; set; }
    public ICollection<EducationResponseDTO>? Educations { get; set; }
    public BankAccountResponseDTO? BankAccount { get; set; }
    public EmergencyContactResponseDTO? EmergencyContact { get; set; }
    public RepresentativeResponseDTO? Repersentative { get; set; }
    public ICollection<WitnessResponseDTO>? Witnesses { get; set; }
    public ICollection<BeneficiaryResponseDTO>? Beneficiaries { get; set; }
    public ICollection<FileCollectionResponseDTO>? FileCollections { get; set; }
    public AddressResponseDTO? Address { get; set; }
}