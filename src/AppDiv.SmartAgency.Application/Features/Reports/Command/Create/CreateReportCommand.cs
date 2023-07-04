

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ReportRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

public record CreateReportCommand(string ReportName, string query) : IRequest<ServiceResponse<Int32>> { }
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

        response = await _reportRepository.CreateReportAsync(command.ReportName, command.query);
        return response;
    }
}