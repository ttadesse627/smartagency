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

    public GetAllRequestedQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<SearchModel<ApplicantsResponseDTO>> Handle(GetAllRequestedQuery request, CancellationToken cancellationToken)
    {
        var applicantList = await _applicantRepository.GetAllWithPredicateSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, appl => appl.IsDeleted == false && appl.IsRequested == true, "Partner");
        var response = CustomMapper.Mapper.Map<SearchModel<ApplicantsResponseDTO>>(applicantList);

        return response;
    }
}