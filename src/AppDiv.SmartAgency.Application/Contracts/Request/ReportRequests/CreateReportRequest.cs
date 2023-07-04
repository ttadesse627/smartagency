

using AppDiv.SmartAgency.Utility.Contracts;
using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
public record CreateReportRequest
{
    public string ReportName { get; set; }
    public string ReportQuery { get; set; }

}