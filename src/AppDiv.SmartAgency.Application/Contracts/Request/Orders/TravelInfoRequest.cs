
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record TravelInfoRequest
{
    public DateTime? RegisteredDate { get; set; }
    public string? TicketNumber { get; set; }
    public DateTime? FlightDate { get; set; }
    public string? DepartureTime { get; set; }
    public string? Transit { get; set; }
    public string? ArrivalTime { get; set; }
    public Guid? AirLineId { get; set; }
    public Guid? TicketOfficeId { get; set; }
    public string? TicketPrice { get; set; }
    public string? Remark { get; set; }
}