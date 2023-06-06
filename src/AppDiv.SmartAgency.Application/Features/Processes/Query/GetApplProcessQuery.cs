
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetApplProcessQuery : IRequest<ApplicantProcessResponseDTO>
{
    public Guid Id { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetApplProcessQuery(Guid id, int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
    {
        Id = id;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }
}
public class GetApplProcessQueryHandler : IRequestHandler<GetApplProcessQuery, ApplicantProcessResponseDTO>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IProcessDefinitionRepository _definitionRepository;
    public GetApplProcessQueryHandler(IApplicantRepository applicantRepository, IProcessRepository processRepository, IProcessDefinitionRepository definitionRepository)
    {
        _applicantRepository = applicantRepository;
        _processRepository = processRepository;
        _definitionRepository = definitionRepository;
    }
    public async Task<ApplicantProcessResponseDTO> Handle(GetApplProcessQuery query, CancellationToken cancellationToken)
    {
        var applicantLoadedProperties = new string[] { "ApplicantProcesses", "Order", "Order.Sponsor" };
        var processLoadedProperties = new string[] { "ApplicantProcesses", "ApplicantProcesses.Applicant",
                                    "ApplicantProcesses.Applicant.Order", "ApplicantProcesses.Applicant.Order.Sponsor" };
        var response = new ApplicantProcessResponseDTO();

        if (query.Id != null)
        {
            var process = await _processRepository.GetAsync(query.Id);
            if (process.Step == 1)
            {
                var notStartedApplicants = await _applicantRepository.GetAllApplWithPredicateSrchAsync(
                    query.PageNumber, query.PageSize, query.SearchTerm, query.OrderBy, query.SortingDirection,
                    appl => appl.ApplicantProcesses == null || appl.ApplicantProcesses.Count == 0, applicantLoadedProperties);
                var initAppls = new List<GetApplProcessResponseDTO>();
                foreach (var notStrtAppl in notStartedApplicants.Items)
                {
                    initAppls.Add(new GetApplProcessResponseDTO()
                    {
                        PassportNumber = notStrtAppl.PassportNumber,
                        FullName = notStrtAppl.FirstName + " " + notStrtAppl.MiddleName + " " + notStrtAppl.LastName,
                        OrderNumber = notStrtAppl.Order?.OrderNumber!,
                        SponsorName = notStrtAppl.Order?.Sponsor?.FullName!
                    });
                }

                response.NotStartedApplicants = initAppls;

                var onProcessApplicants = await _definitionRepository.GetAllWithPredicateSearchAsync(
                    query.PageNumber, query.PageSize, query.SearchTerm, query.OrderBy, query.SortingDirection,
                    null, processLoadedProperties);

                // foreach (var proDef in onProcessApplicants.Items)
                // {
                //     var pdApplicants = new List<GetApplProcessResponseDTO>();
                //     foreach (var applicant in proDef.ApplicantProcesses)
                //     {
                //         pdApplicants.Add(new GetApplProcessResponseDTO()
                //         {
                //             PassportNumber = applicant.Applicant.PassportNumber,
                //             FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                //             OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                //             SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
                //         });
                //     }
                //     response.ProcessDefinitions?.Add(new GetProcessDefinitionResponseDTO()
                //     {
                //         Id = proDef.Id,
                //         Name = proDef.Name,
                //         Step = proDef.Step,
                //         ApplicantProcesses = pdApplicants
                //     });
                // }

                response.ProcessDefinitions = CustomMapper.Mapper.Map<List<GetProcessDefinitionResponseDTO>>(onProcessApplicants.Items);
            }
            else
            {
                var onProcessApplicants = await _definitionRepository.GetAllWithPredicateSearchAsync(
                    query.PageNumber, query.PageSize, query.SearchTerm, query.OrderBy, query.SortingDirection,
                    null, processLoadedProperties);

                response.ProcessDefinitions = CustomMapper.Mapper.Map<List<GetProcessDefinitionResponseDTO>>(onProcessApplicants.Items);
            }
        }
        else { }
        return response;
    }
}