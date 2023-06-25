

using AppDiv.SmartAgency.Utility.Contracts;
using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
public record ReportsQueryRequest
{
    public List<string>? Columns { get; set; }
    public List<Filter>? Filters { get; set; }
    public List<Aggregate>? Aggregates { get; set; }

}