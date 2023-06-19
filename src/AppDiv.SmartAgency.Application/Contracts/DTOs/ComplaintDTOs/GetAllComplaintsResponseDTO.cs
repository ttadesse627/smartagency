

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
public record GetAllComplaintsResponseDTO
{
    public Guid Id { get; set; }
    public string? SponsorName { get; set; }
    public string? EmployeeName { get; set; }
    public int Days { get; set; }
}