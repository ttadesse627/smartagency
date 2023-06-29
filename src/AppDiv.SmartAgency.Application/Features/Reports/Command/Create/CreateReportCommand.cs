

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

public record CreateReportCommand(string ReportName, string ReportType, ReportsQueryRequest query) : IRequest<ServiceResponse<Int32>> { }
public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, ServiceResponse<Int32>>
{
    private readonly IReportsRepository _reportRepository;
    public CreateReportCommandHandler(IReportsRepository reportsRepository)
    {
        _reportRepository = reportsRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateReportCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();

        response = await _reportRepository.CreateReport(command.ReportName, command.ReportType, command.query.Columns, command.query.Filters, command.query.Aggregates);
        return response;
    }
}