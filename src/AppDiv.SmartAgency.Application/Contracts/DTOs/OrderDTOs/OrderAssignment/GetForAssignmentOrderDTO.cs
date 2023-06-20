

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
public record GetForAssignmentOrderDTO
{
    public virtual Guid OrderId { get; set; }
    public string? OrderNumber { get; set; }
    public string? VisaNumber { get; set; }
    public string? FullName { get; set; }
    public string? JobTitle { get; set; }
    public string? Religion { get; set; }
    public string? Language { get; set; }
    public int? Age { get; set; }
}