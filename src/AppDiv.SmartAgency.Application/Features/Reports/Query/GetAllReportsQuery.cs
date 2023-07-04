
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query
{
    public record GetAllReportsQuery(string ReportName, ReportsQueryRequest? query = null) : IRequest<JObject> { }
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, JObject>
    {
        private readonly IReportsRepository _reportsRepository;
        public GetAllReportsQueryHandler(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<JObject> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            var response = await _reportsRepository.GetReportData(request.ReportName, request.query.Columns, request.query.Filters, request.query.Aggregates);
            return response;
        }
    }

}