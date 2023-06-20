

namespace AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
public record FilterQuery
{
    public string? Property { get; set; }
    public string? Condition { get; set; }
    public object? Value { get; set; }
}