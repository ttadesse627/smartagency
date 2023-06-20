

using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
public record ReportsQueryRequest
{
    public string? ObjectName { get; set; }
    public ICollection<FilterQuery> Filters { get; set; }
    public ICollection<AggregateQuery> Aggregates { get; set; }

}