

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
public record GetForAssignmentDTO
{
    public ICollection<GetApplForAssignmentDTO> UnAssignedApplicants { get; set; }
    public ICollection<GetForAssignmentOrderDTO> UnAssignedOrders { get; set; }
}