// using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
// using AppDiv.SmartAgency.Application.Interfaces.Persistence;
// using MediatR;

// namespace AppDiv.SmartAgency.Application.Features.Reports.Query
// {
//     public record GetTestDataQuery { }
//     public class GetTestDataQueryHandler : IRequestHandler<GetTestDataQuery>
//     {
//         private readonly IGetReportsRepository _reportsRepository;
//         public GetTestDataQueryHandler(IGetReportsRepository reportsRepository)
//         {
//             _reportsRepository = reportsRepository;
//         }

//         public async Task Handle(GetTestDataQuery request, CancellationToken cancellationToken)
//         {
//             await _reportsRepository.GetTestData();
//         }

//         Task<Unit> IRequestHandler<GetTestDataQuery, Unit>.Handle(GetTestDataQuery request, CancellationToken cancellationToken)
//         {
//             throw new NotImplementedException();
//         }
//     }

// }