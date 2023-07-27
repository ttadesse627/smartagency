using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Queries;
public class GetAllRequestedQuery : IRequest<SearchModel<ApplicantsResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetAllRequestedQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }
}
public class GetAllRequestedQueryHandler : IRequestHandler<GetAllRequestedQuery, SearchModel<ApplicantsResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IRequestedApplicantRepository _requestedApplicantRepository;

    public GetAllRequestedQueryHandler(IApplicantRepository applicantRepository, IRequestedApplicantRepository requestedApplicantRepository)
    {
        _applicantRepository = applicantRepository;
        _requestedApplicantRepository = requestedApplicantRepository;
    }
    public async Task<SearchModel<ApplicantsResponseDTO>> Handle(GetAllRequestedQuery request, CancellationToken cancellationToken)
    {
        var response = new SearchModel<ApplicantsResponseDTO>();
        var applicantList = await _requestedApplicantRepository.GetAllWithPredicateSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, appl => !appl.Applicant.IsDeleted, "Applicant", "Partner", "Applicant.MaritalStatus", "Applicant.Religion", "Applicant.BrokerName");
        ICollection<ApplicantsResponseDTO> requestedApplicants = new List<ApplicantsResponseDTO>();

        if (applicantList.Items.Any())
        {
            foreach (var applicant in applicantList.Items)
            {
                if (applicant != null)
                {
                    var applicantResponse = new ApplicantsResponseDTO
                    {
                        Id = applicant.Applicant.Id,
                        FirstName = applicant.Applicant.FirstName,
                        MiddleName = applicant.Applicant.MiddleName,
                        LastName = applicant.Applicant.LastName,
                        RegisteredDate = applicant.Applicant.CreatedAt,
                        PassportNumber = applicant.Applicant.PassportNumber,
                        Gender = applicant.Applicant.Gender,
                        MaritalStatus = applicant.Applicant.MaritalStatus?.Value!,
                        Religion = applicant.Applicant.Religion?.Value,
                        BrokerName = applicant.Applicant.BrokerName?.Value
                    };
                    requestedApplicants.Add(applicantResponse);
                }
            }
            response.CurrentPage = applicantList.CurrentPage;
            response.Filters = applicantList.Filters;
            response.Items = requestedApplicants;
            response.MaxPage = applicantList.MaxPage;
            response.PagingSize = applicantList.PagingSize;
            response.SearchKeyWord = applicantList.SearchKeyWord;
            response.SortingDirection = applicantList.SortingDirection;
            response.SortingColumn = applicantList.SortingColumn;
            response.TotalCount = applicantList.TotalCount;

        }


        return response;
    }
}