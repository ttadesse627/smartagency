using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class GetReportsRepository : IGetReportsRepository
    {

        private readonly SmartAgencyDbContext _context;
        public GetReportsRepository(SmartAgencyDbContext dbContext)
        {
            _context = dbContext;
        }
        // public async Task<List<Object>> GetReport(ReportsQueryRequest query)
        // {
        //     var sql = $"SELECT {query.Filters} FROM {query.ObjectName} WHERE ";
        //     /* var result = _dbContext.DynamicView.FromSqlRaw(sql).ToList();
        //     return result.Select(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();*/

        //     // var result = _context.partner_view.FromSqlRaw("SELECT * FROM partner_view").ToList();
        //     // return result;
        // }
    }
}