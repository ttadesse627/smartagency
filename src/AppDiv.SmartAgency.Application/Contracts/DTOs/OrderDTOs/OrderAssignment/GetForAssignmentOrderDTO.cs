

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
public record GetForAssignmentOrderDTO
{
    public virtual Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string VisaNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public GetOrderCriteriaRespDTO? OrderCriteria { get; set; }
    public GetSponsorResponseDTO? Sponsor { get; set; }
}