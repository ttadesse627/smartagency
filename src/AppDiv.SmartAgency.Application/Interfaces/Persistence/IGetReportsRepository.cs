
using AppDiv.SmartAgency.Utility.Contracts;
namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IGetReportsRepository
    {
        public List<string> GetObjectTypes();
        public Task<(IEnumerable<Dictionary<string, object>>, List<String>)> GetReportData(string reportName, List<string>? columns = null, List<Filter>? filters = null, List<Aggregate>? aggregates = null);
        Task<List<DbTable>> GetDatabaseTablesAsync();
        Task<IEnumerable<Dictionary<string, object>>> GetTestData();
    }
}