

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Reports.Command.Create;
public record CreateReportCommand(string ReportName, string Query) : IRequest<ServiceResponse<int>> { }
public class CreateReportCommandHandler(IReportsRepository reportsRepository) : IRequestHandler<CreateReportCommand, ServiceResponse<int>>
{
    private readonly IReportsRepository _reportRepository = reportsRepository;

    public async Task<ServiceResponse<int>> Handle(CreateReportCommand command, CancellationToken cancellationToken)
    {
        var response = await _reportRepository.CreateReportAsync(command.ReportName, command.Query);
        return response;
    }
}