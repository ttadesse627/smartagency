

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ReportDTOs;
public record ApplicantReportResponseDTO
{
    public string PassportNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public DateTime IssuedDate { get; set; }
    public int FileNumber { get; set; }
    public string FullName { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string PlaceOfBirth { get; set; }
    public string? MotherName { get; set; }
    public string? PreviousNationality { get; set; }
    public string? CurrentNationality { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? PassportIssuedPlace { get; set; }
    public string? MaritalStatus { get; set; }
    public string? Religion { get; set; }
    public string? Profession { get; set; }
    public string? Experience { get; set; }
    public string? Language { get; set; }
    public string? BrokerName { get; set; }
}