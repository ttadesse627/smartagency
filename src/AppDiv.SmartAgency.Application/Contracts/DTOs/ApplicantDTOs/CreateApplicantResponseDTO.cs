using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
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
    public LookUpResponseDTO? IssuingCountry { get; set; }
    public DateTime IssuedDate { get; set; }
    public LookUpResponseDTO? IssuedPlace { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? AmharicFullName { get; set; }
    public string? ArabicFullName { get; set; }
    public LookUpResponseDTO? MaritalStatus { get; set; }
    public string? Complexion { get; set; }
    public int NumberOfChildren { get; set; }
    public LookUpResponseDTO? Health { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public ICollection<LookUpResponseDTO>? JobTitles { get; set; }
    public LookUpResponseDTO? Salary { get; set; }
    public LookUpResponseDTO? DesiredCountry { get; set; }
    public string? MotherFullName { get; set; }
    public string? PreviousCountry { get; set; }
    public string? CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int ContractPeriod { get; set; }
    public string? JobTitleAmharic { get; set; }
    public LookUpResponseDTO? BrokerName { get; set; }
    public LookUpResponseDTO? Branch { get; set; }
    public string? Remark { get; set; }
    public PartnerApplRespDTO? Partner { get; set; }
    public ICollection<LanguageResponseDTO>? Languages { get; set; }
    public ICollection<LookUpResponseDTO>? TechnicalSkills { get; set; }
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