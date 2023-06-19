using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query
{
    public class GetAllReportsQuery: IRequest<List<Object>>
    {
       public string TableName { get; set; }
       public string ColumnName { get; set; }

       public GetAllReportsQuery(string tableName, string columnName)
       {
            TableName = tableName;
            ColumnName = columnName;

       }
    }
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, List<Object>>
    {
        private readonly IGetReportsRepository _reportsRepository;
        public GetAllReportsQueryHandler(IGetReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<List<Object>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            var response = await _reportsRepository.GetReport(request.TableName, request.ColumnName);
            return response;
        }


    }

}