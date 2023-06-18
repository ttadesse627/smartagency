
namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record StepbackProcessRequest
{
    public Guid? ProcessDefinitionId { get; set; }
    public Guid? ApplicantId { get; set; }
}