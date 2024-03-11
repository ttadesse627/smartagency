

namespace AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
public record EnjazRequest
{
    public string? ApplicationNumber { get; set; }
    public string? TransactionCode { get; set; }
}