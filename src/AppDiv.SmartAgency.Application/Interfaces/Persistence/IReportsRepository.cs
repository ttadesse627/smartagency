
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IReportsRepository
    {
        Task<JObject> GetReports();
        Task<ServiceResponse<int>> CreateReport(string reportName, string tableName, List<string>? columns = null, List<Filter>? filters = null, List<Aggregate>? aggregates = null);
        Task<ServiceResponse<int>> CreateReportAsync(string reportName, string query);
        Task<JObject> GetReportData(string reportName, List<string>? columns = null, List<Filter>? filters = null, List<Aggregate>? aggregates = null);
    }
}