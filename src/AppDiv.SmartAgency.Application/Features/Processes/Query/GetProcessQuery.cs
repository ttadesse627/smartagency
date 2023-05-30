

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public class GetProcessQuery : IRequest<ServiceResponse<List<GetProcessResponseDTO>>> { }
public class GetProcessQueryHandler : IRequestHandler<GetProcessQuery, ServiceResponse<List<GetProcessResponseDTO>>>
{
    private readonly IProcessRepository _processRepository;
    public GetProcessQueryHandler(IProcessRepository processRepository)
    {
        _processRepository = processRepository;
    }
    public async Task<ServiceResponse<List<GetProcessResponseDTO>>> Handle(GetProcessQuery query, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<List<GetProcessResponseDTO>>();
        var processes = await _processRepository.GetAllWithAsync("ProcessDefinitions");
        if (processes.Count() > 0)
        {
            response.Success = true;
            response.Data = CustomMapper.Mapper.Map<List<GetProcessResponseDTO>>(processes);
            response.Message = $"{response.Data.Count} record/s is/are fetched!";
        }
        else
        {
            response.Message = "No Record Found";
            response.Success = false;
        }
        return response;
    }
}