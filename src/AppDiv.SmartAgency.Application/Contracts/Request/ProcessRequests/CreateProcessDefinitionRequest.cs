

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record CreateProcessDefinitionRequest
{
    public string? Name { get; set; }
    public int Step { get; set; }
    public int ExpiryInterval { get; set; }
    public bool RequestApproval { get; set; }
}