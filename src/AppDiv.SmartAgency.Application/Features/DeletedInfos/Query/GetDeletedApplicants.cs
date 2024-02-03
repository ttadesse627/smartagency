
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.DeletedInfos.Query;

public class GetDeletedApplicants : IRequest<SearchModel<ApplicantsResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
    public string? OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetDeletedApplicants(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }
}

public class GetDeletedApplicantsHandler : IRequestHandler<GetDeletedApplicants, SearchModel<ApplicantsResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetDeletedApplicantsHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<SearchModel<ApplicantsResponseDTO>> Handle(GetDeletedApplicants request, CancellationToken cancellationToken)
    {
        var eagerLoadedProperties = new string[] { };
        var applicantList = await _applicantRepository.GetAllWithPredicateSearchAsync
                        (
                            request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy,
                            request.SortingDirection, appl => appl.IsDeleted == true, eagerLoadedProperties
                        );
        var applicantResponse = CustomMapper.Mapper.Map<SearchModel<ApplicantsResponseDTO>>(applicantList);
        return applicantResponse;
    }
}