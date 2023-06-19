
namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitApplicantProcessRequest
{
    public Guid? ProcessDefinitionId { get; set; }
    public Guid? ApplicantId { get; set; }
    public DateTime? Date { get; set; }
}