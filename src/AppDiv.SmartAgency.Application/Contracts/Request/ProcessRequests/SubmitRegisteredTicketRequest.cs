
namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitRegisteredTicketRequest
{
    public string? TicketNumber { get; set; }
    public DateTime? FlightDate { get; set; }
    public string? DepartureTime { get; set; }
    public string? Transit { get; set; }
    public string? ArrivalTime { get; set; }
    public string? Remark { get; set; }
    public string? TicketPrice { get; set; }
    public DateTime Date { get; set; }
    public Guid? AirLineId { get; set; }
    public Guid? ProcessDefinitionId { get; set; }
    public Guid ApplicantId { get; set; }
}