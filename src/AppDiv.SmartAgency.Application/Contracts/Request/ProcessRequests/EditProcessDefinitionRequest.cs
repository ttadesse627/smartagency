

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record EditProcessDefinitionRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Step { get; set; }
    public bool RequestApproval { get; set; }
}