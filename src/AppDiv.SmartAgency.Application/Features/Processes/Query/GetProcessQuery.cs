

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public class GetProcessQuery : IRequest<ResponseContainerDTO<List<GetProcessResponseDTO>>> { }
public class GetProcessQueryHandler : IRequestHandler<GetProcessQuery, ResponseContainerDTO<List<GetProcessResponseDTO>>>
{
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantRepository;
    public GetProcessQueryHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantRepository)
    {
        _processRepository = processRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ResponseContainerDTO<List<GetProcessResponseDTO>>> Handle(GetProcessQuery query, CancellationToken cancellationToken)
    {
        var response = new ResponseContainerDTO<List<GetProcessResponseDTO>>();
        var excLoadedProps = new string[] { "Country", "ProcessDefinitions" };
        var processes = await _processRepository.GetAllWithAsync(excLoadedProps);
        if (processes.Count() > 0)
        {
            response.Items = CustomMapper.Mapper.Map<List<GetProcessResponseDTO>>(processes);

        }
        return response;
    }
}