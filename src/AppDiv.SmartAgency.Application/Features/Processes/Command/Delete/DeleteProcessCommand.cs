

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record DeleteProcessCommand(Guid Id) : IRequest<ServiceResponse<Int32>> { }
public class DeleteProcessCommandHandler : IRequestHandler<DeleteProcessCommand, ServiceResponse<Int32>>
{
    private readonly IProcessRepository _processRepository;
    public DeleteProcessCommandHandler(IProcessRepository processRepository)
    {
        _processRepository = processRepository;
    }
    public async Task<ServiceResponse<int>> Handle(DeleteProcessCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();

        await _processRepository.DeleteAsync(command.Id);
        try
        {
            response.Success = await _processRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                response.Message = "Successfully deleted the process.";
            }
        }
        catch (Exception ex)
        {
            response.Errors?.Add(ex.Message);
        }

        return response;
    }
}