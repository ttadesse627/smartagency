
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query
{
    public record GetReportObjectsQuery : IRequest<List<DbTable>> { }
    public class GetReportObjectsQueryHandler : IRequestHandler<GetReportObjectsQuery, List<DbTable>>
    {
        private readonly IGetReportsRepository _reportsRepository;
        public GetReportObjectsQueryHandler(IGetReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<List<DbTable>> Handle(GetReportObjectsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DbTable>();
            response = await _reportsRepository.GetDatabaseTablesAsync();
            return response;
        }
    }

}