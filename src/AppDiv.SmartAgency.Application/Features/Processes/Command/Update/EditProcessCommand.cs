

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record EditProcessCommand(EditProcessRequest request) : IRequest<ServiceResponse<Int32>> { }
public class EditProcessCommandHandler : IRequestHandler<EditProcessCommand, ServiceResponse<Int32>>
{
    private readonly IProcessRepository _processRepository;
    public EditProcessCommandHandler(IProcessRepository processRepository)
    {
        _processRepository = processRepository;
    }
    public async Task<ServiceResponse<int>> Handle(EditProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new ServiceResponse<Int32>();

        var processEntity = await _processRepository.GetWithPredicateAsync(pr => pr.Id == request.Id, "ProcessDefinitions");
        if (processEntity != null)
        {
            // foreach (var pd in request.ProcessDefinitions!)
            // {
            //     var equality = false;
            //     foreach (var pde in processEntity.ProcessDefinitions!)
            //     {
            //         if (pde.Id == pd.Id)
            //         {

            //         }
            //     }
            // }
            CustomMapper.Mapper.Map(request, processEntity);
            try
            {
                response.Success = await _processRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Message = "Successfully updated the process.";
                }
            }
            catch (Exception ex)
            {
                // Handle any database errors and add them to the exceptions list
                response.Errors?.Add(ex.Message);
            }
        }

        return response;
    }
}