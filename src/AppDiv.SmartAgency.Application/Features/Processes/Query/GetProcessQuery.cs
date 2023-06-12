

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
    private readonly IApplicantProcessRepository _applicantRepository;
    public GetProcessQueryHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantRepository)
    {
        _processRepository = processRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<List<GetProcessResponseDTO>>> Handle(GetProcessQuery query, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<List<GetProcessResponseDTO>>();
        var excLoadedProps = new string[] { "Country", "ProcessDefinitions" };
        var processes = await _processRepository.GetAllWithAsync(excLoadedProps);
        if (processes.Count() > 0)
        {
            // foreach (var process in processes)
            // {
            //     var pdResponse = CustomMapper.Mapper.Map<GetPDResponseDTO>(process.ProcessDefinitions);
            // }
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