using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Query
{
    public record GetAllReportsQuery(ReportsQueryRequest query) : IRequest<List<Object>> { }
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, List<Object>>
    {
        private readonly IGetReportsRepository _reportsRepository;
        public GetAllReportsQueryHandler(IGetReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<List<Object>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<Object>();
            // response = await _reportsRepository.GetReport(request.query);
            return response;
        }


    }

}