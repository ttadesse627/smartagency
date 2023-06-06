

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record CreateProcessCommand(CreateProcessRequest request) : IRequest<ServiceResponse<Int32>>
{

}
public class CreateProcessCommandHandler : IRequestHandler<CreateProcessCommand, ServiceResponse<Int32>>
{
    private readonly IProcessRepository _processRepository;
    public CreateProcessCommandHandler(IProcessRepository processRepository)
    {
        _processRepository = processRepository;
    }
    public async Task<ServiceResponse<int>> Handle(CreateProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new ServiceResponse<Int32>();
        var process = CustomMapper.Mapper.Map<Process>(request);
        try
        {
            await _processRepository.InsertAsync(process, cancellationToken);
            response.Success = await _processRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                response.Message = "Added Successfully!";
                response.Success = true;
            }
        }
        catch (Exception ex)
        {

            throw new ApplicationException(ex.Message);
        }
        return response;
    }
}