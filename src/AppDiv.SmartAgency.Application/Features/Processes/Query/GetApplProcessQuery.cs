
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetApplProcessQuery : IRequest<List<GetProcessDefinitionResponseDTO>>
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
public class GetApplProcessQueryHandler : IRequestHandler<GetApplProcessQuery, List<GetProcessDefinitionResponseDTO>>
{
    private readonly IProcessRepository _processRepository;
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IApplicantRepository _applicantRepository;

    public GetApplProcessQueryHandler(IApplicantRepository applicantRepository, IProcessRepository processRepository, IProcessDefinitionRepository definitionRepository)
    {
        _applicantRepository = applicantRepository;
        _processRepository = processRepository;
        _definitionRepository = definitionRepository;
    }
    public async Task<List<GetProcessDefinitionResponseDTO>> Handle(GetApplProcessQuery query, CancellationToken cancellationToken)
    {
        var applicantLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var pdLoadedProperties = new string[] { "ApplicantProcesses", "ApplicantProcesses.Applicant.Order" };

        var response = new List<GetProcessDefinitionResponseDTO>();

        if (query.Id != null)
        {
            var processEntity = await _processRepository.GetWithPredicateAsync(pro => pro.Id == query.Id, "ProcessDefinitions");
            // if (processEntity.Step == 1)
            // {
            //     var notStartedApplicants = await _applicantRepository.GetAllApplWithPredicateSrchAsync(
            //         query.PageNumber, query.PageSize, query.SearchTerm, query.OrderBy, query.SortingDirection,
            //         appl => appl.ApplicantProcesses == null || appl.ApplicantProcesses.Count == 0, applicantLoadedProperties);
            //     var initAppls = new List<GetApplProcessResponseDTO>();
            //     foreach (var notStrtAppl in notStartedApplicants.Items)
            //     {
            //         initAppls.Add(new GetApplProcessResponseDTO()
            //         {
            //             Id = notStrtAppl.Id,
            //             PassportNumber = notStrtAppl.PassportNumber,
            //             FullName = notStrtAppl.FirstName + " " + notStrtAppl.MiddleName + " " + notStrtAppl.LastName,
            //             OrderNumber = notStrtAppl.Order?.OrderNumber!,
            //             SponsorName = notStrtAppl.Order?.Sponsor?.FullName!
            //         });
            //     }

            //     response.ProcessReadyApplicants = initAppls;
            // }
            // else
            // {
            var onProcessApplicants = await _definitionRepository.GetAllWithPredicateSearchAsync(
                query.PageNumber, query.PageSize, query.SearchTerm, query.OrderBy, query.SortingDirection,
                pd => pd.ApplicantProcesses.All(applPr => applPr.Status == ProcessStatus.In) && pd.ProcessId == query.Id, pdLoadedProperties);

            foreach (var proDef in onProcessApplicants.Items)
            {
                var pdApplicants = new SearchModel<GetApplProcessResponseDTO>();
                foreach (var applicant in proDef.ApplicantProcesses)
                {
                    pdApplicants.Items.Append(new GetApplProcessResponseDTO()
                    {
                        Id = applicant.Applicant.Id,
                        PassportNumber = applicant.Applicant.PassportNumber,
                        FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                        OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                        SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
                    });
                }
                response.Add(new GetProcessDefinitionResponseDTO()
                {
                    Id = proDef.Id,
                    Name = proDef.Name,
                    Step = proDef.Step,
                    ApplicantProcesses = pdApplicants
                });
            }

            response = CustomMapper.Mapper.Map<List<GetProcessDefinitionResponseDTO>>(onProcessApplicants.Items);
            // }
        }
        return response;
    }
}