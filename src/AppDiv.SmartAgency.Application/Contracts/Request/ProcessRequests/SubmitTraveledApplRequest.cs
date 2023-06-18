

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record SubmitTraveledApplRequest
{
    public Guid? ApplicantId { get; set; }
    public DateTime? Date { get; set; }
    public string? Remark { get; set; }
}