

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
public record TravelInfoResponseDTO
{
 
    public string? TicketNumber { get; set; }
    public DateTime? FlightDate { get; set; }
    public string? DepatrureFromAddis { get; set; }
    public string? Transit { get; set; }
    public string? ArrivalTime { get; set; }
    public string? AirLine { get; set; }
    public string? TicketOffice { get; set; }
    public string? TicketPrice { get; set; }
    public string? UploadTicket {get; set;}
    
}