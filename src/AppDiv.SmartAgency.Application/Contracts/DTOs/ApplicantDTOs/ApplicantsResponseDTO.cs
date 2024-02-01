
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ApplicantsResponseDTO
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime? RegisteredDate { get; set; }
    public string? PassportNumber { get; set; }
    public Gender? Gender { get; set; }
    public required string MaritalStatus { get; set; }
    public string? Religion { get; set; }
    public string? BrokerName { get; set; }
    public decimal? Expense { get; set; }
}