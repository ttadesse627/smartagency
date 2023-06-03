
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
public record ApplicantsResponseDTO
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? PassportNumber { get; set; }
    public Gender? Gender { get; set; }
    public LookUpResponseDTO? MaritalStatus { get; set; }
    public LookUpResponseDTO? Religion { get; set; }
    public LookUpResponseDTO? BrokerName { get; set; }
    public decimal? Expense { get; set; }
}