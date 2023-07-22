namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitTicketRefundRequest
{
    public int DateInterval { get; set; }
    public ICollection<Guid> ApplicantIds { get; set; }
    public DateTime? Date { get; set; }
}