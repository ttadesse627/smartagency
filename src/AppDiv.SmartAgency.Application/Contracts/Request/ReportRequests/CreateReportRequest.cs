

namespace AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
public record CreateReportRequest
{
    public required string ReportName { get; set; }
    public required string ReportQuery { get; set; }

}