

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
public record GetOrderComplaintsResponseDTO
{
    public SponsorInfoDTO? SponsorInfo { get; set; }
    public EmployeeInfoDTO? EmployeeInfo { get; set; }
    public ICollection<GetComplaintResponseDTO>? Complaints { get; set; }

}