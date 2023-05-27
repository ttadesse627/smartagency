

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record CreateProcessRequest
{
    public string? Name { get; set; }
    public string? Step { get; set; }
}