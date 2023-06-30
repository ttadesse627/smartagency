


using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetProcessDefinitionsQuery(Guid id) : IRequest<ResponseContainerDTO<List<GetProcessDefinitionResponseDTO>>> { }
public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, ResponseContainerDTO<List<GetProcessDefinitionResponseDTO>>>
{
    private readonly IProcessDefinitionRepository _definitionRepository;
    public GetProcessDefinitionsQueryHandler(IProcessDefinitionRepository definitionRepository)
    {
        _definitionRepository = definitionRepository;
    }
    public async Task<ResponseContainerDTO<List<GetProcessDefinitionResponseDTO>>> Handle(GetProcessDefinitionsQuery query, CancellationToken cancellationToken)
    {
        var explicitLoadedProperties = new string[]
        {
            "ApplicantProcesses", "ApplicantProcesses.Applicant",
            "ApplicantProcesses.Applicant.Order", "ApplicantProcesses.Applicant.Order.Sponsor",
        };
        var response = new ResponseContainerDTO<List<GetProcessDefinitionResponseDTO>>();
        var processes = await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == query.id, explicitLoadedProperties);
        if (processes.Count() > 0)
        {
            response.Items = CustomMapper.Mapper.Map<List<GetProcessDefinitionResponseDTO>>(processes);
        }
        return response;
    }
}