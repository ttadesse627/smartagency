
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record EditOrderStatusRequest
{
    public Guid ApplicantId { get; set; }
    public ICollection<OrderStatusRequest>? Statuses { get; set; }
    public EnjazRequest? Enjaz { get; set; }
    public TravelInfoRequest? TravelInfo { get; set; }
}