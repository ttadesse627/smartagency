

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
public record DropdownEnjazResponseDTO
{
    public Guid? ApplicantId { get; set; }
    public string? OrderNumber { get; set; }
    public string? SponsorFullName { get; set; }
    public string? EmployeeProfession { get; set; }
    public string? EmployeeLanguage { get; set; }
    public string? PassportNumber { get; set; }
    public string? EmployeeFullName { get; set; }
}