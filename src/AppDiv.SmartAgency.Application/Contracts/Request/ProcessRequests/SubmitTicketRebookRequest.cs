namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitTicketRebookRequest
{
    public int DateInterval { get; set; }
    public Guid? ProcessDefinitionId { get; set; }
    public Guid? ApplicantId { get; set; }
    public DateTime? Date { get; set; }
}