

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
public record EmployeeInfoDTO
{
    public Guid Id { get; set; }
    public string? EmployeeName { get; set; }
    public string? HouseNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
}