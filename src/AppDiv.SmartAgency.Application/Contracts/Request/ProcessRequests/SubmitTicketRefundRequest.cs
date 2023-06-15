namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitTicketRefundRequest
{
    public int DateInterval { get; set; }
    public Guid? ApplicantId { get; set; }
    public DateTime? Date { get; set; }
}