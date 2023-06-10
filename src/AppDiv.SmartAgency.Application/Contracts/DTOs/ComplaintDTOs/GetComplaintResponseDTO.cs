

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
public record GetComplaintResponseDTO
{
    public string? Message { get; set; }
    public string? SenderName { get; set; }
    public DateTime? Date { get; set; }
}