using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IGetReportsRepository
    {
       Task<List<Object>> GetReport(string tableName, string columnName);
    }
}