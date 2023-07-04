

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetProcessDefinitionsQuery(Guid id) : IRequest<ProcessDetailsResponseDTO> { }
public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, ProcessDetailsResponseDTO>
{
    private readonly IProcessRepository _processRepository;
    public GetProcessDefinitionsQueryHandler(IProcessRepository processRepository)
    {
        _processRepository = processRepository;
    }
    public async Task<ProcessDetailsResponseDTO> Handle(GetProcessDefinitionsQuery query, CancellationToken cancellationToken)
    {
        var explicitLoadedProperties = new string[]
        {
            "ProcessDefinitions", "Country",
        };
        var response = new ProcessDetailsResponseDTO();
        var process = await _processRepository.GetWithPredicateAsync(pd => pd.Id == query.id, explicitLoadedProperties);
        if (process != null)
        {
            response = CustomMapper.Mapper.Map<ProcessDetailsResponseDTO>(process);
        }
        return response;
    }
}