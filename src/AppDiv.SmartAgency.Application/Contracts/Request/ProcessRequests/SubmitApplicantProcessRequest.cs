
namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitApplicantProcessRequest
{
    public Guid PdId { get; set; }
    public ICollection<Guid> ApplicantIds { get; set; }
    public Guid NextPdId { get; set; }
    public DateTime Date { get; set; }
}