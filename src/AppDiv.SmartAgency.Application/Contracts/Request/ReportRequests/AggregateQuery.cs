

namespace AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
public record AggregateQuery
{
    public string? Property { get; set; }
    public string? Method { get; set; }
}