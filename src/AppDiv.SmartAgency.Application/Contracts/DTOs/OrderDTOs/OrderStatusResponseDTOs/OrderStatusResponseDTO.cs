
namespace AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusResponseDTOs;
public record OrderStatusResponseDTO
{
    public Guid? EmployeeId { get; set; }
    public string? OrderNumber { get; set; }
    public string? ClientName { get; set; }
    public string? VisaNumber { get; set; }
    public string? EmployeeFullName { get; set; }
    public string? PassportNumber { get; set; }
    public string? SponsorName { get; set; }
    public string? BrokerName { get; set; }
    // public int Days { get; set; }
    // public int NumberOfDays { get; set; }
    // public int Left { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public string? Priority { get; set; }
    public string? Jobtitle { get; set; }
    // public ICollection<StatusResponseDTO>? Statuses { get; set; }
    public string? PortOfArrival { get; set; }
    public string? TicketNo { get; set; }
    public DateTime? FlightDate { get; set; }
    public string? DepartureTime { get; set; }
    public string? Transit { get; set; }
    public string? ArrivalTime { get; set; }
    public string? TravelStatus { get; set; }
    public string? StatusName { get; set; }
    public DateTime? Date { get; set; }


}