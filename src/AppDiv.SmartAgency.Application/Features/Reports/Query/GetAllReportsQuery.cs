using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query
{
    public record GetAllReportsQuery(string ReportName, ReportsQueryRequest query) : IRequest<(IEnumerable<Dictionary<string, object>>, List<string>)> { }
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, (IEnumerable<Dictionary<string, object>>, List<string>)>
    {
        private readonly IGetReportsRepository _reportsRepository;
        public GetAllReportsQueryHandler(IGetReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<(IEnumerable<Dictionary<string, object>>, List<string>)> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            var response = await _reportsRepository.GetReportData(request.ReportName, request.query.Columns, request.query.Filters, request.query.Aggregates);
            return response;
        }
    }

}