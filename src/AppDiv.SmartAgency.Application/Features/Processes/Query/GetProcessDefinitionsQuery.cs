


using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetProcessDefinitionsQuery(Guid id) : IRequest<ServiceResponse<List<GetProcessDefinitionResponseDTO>>> { }
public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, ServiceResponse<List<GetProcessDefinitionResponseDTO>>>
{
    private readonly IProcessDefinitionRepository _definitionRepository;
    public GetProcessDefinitionsQueryHandler(IProcessDefinitionRepository definitionRepository)
    {
        _definitionRepository = definitionRepository;
    }
    public async Task<ServiceResponse<List<GetProcessDefinitionResponseDTO>>> Handle(GetProcessDefinitionsQuery query, CancellationToken cancellationToken)
    {
        var explicitLoadedProperties = new string[]
        {
            "ApplicantProcess", "ApplicantProcess.Applicant",
            "ApplicantProcess.Applicant.Order", "ApplicantProcess.Applicant.Order.Sponsor",
        };
        var response = new ServiceResponse<List<GetProcessDefinitionResponseDTO>>();
        var processes = await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == query.id, explicitLoadedProperties);
        if (processes.Count() > 0)
        {
            response.Data = CustomMapper.Mapper.Map<List<GetProcessDefinitionResponseDTO>>(processes);
            response.Success = true;
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