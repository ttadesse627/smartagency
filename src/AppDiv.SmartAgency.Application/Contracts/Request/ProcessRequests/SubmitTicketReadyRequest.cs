namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitTicketReadyRequest
{
    public ICollection<Guid> ApplicantIds { get; set; }
    public DateTime Date { get; set; }
    public string? DateInterval { get; set; }
    public Guid? TicketOfficeId { get; set; }
}