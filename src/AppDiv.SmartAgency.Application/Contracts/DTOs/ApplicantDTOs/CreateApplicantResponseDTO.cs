

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
    public ICollection<LookUp> JobTitles { get; set; }
    public decimal Salary { get; set; }
    public string DesiredCountry { get; set; }
    public string MotherFullName { get; set; }
    public string PreviousCountry { get; set; }
    public string CurrentNationality { get; set; }
    public decimal Height { get; set; }
    public int ContractPeriod { get; set; }
    public string? JobTitleAmharic { get; set; }
    public string? BrokerName { get; set; }
    public string? Branch { get; set; }
    public string? Remark { get; set; }
    public Partner Partner { get; set; }
    public ICollection<Language> Languages { get; set; }
    public ICollection<TechnicalSkill> TechnicalSkills { get; set; }
    public ICollection<Experience> Experiences { get; set; }
    public ICollection<Education> Educations { get; set; }
    public BankAccount BankAccount { get; set; }
    public EmergencyContact EmergencyContact { get; set; }
    public Repersentative Repersentative { get; set; }
    public ICollection<Witness> Witnesses { get; set; }
    public ICollection<Beneficiary> Beneficiaries { get; set; }
    public ICollection<AttachmentFile> AttachmentFile { get; set; }
    public Address Address { get; set; }
}