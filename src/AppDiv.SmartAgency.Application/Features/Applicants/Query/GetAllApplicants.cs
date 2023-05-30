using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Queries;
public class GetAllApplicants : IRequest<SearchModel<ApplicantsResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetAllApplicants(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }
}
public class GetAllApplicantsHandler : IRequestHandler<GetAllApplicants, SearchModel<ApplicantsResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ISmartAgencyDbContext _dbContext;

    public GetAllApplicantsHandler(IApplicantRepository applicantRepository, ISmartAgencyDbContext dbContext)
    {
        _applicantRepository = applicantRepository;
        _dbContext = dbContext;
    }
    public async Task<SearchModel<ApplicantsResponseDTO>> Handle(GetAllApplicants request, CancellationToken cancellationToken)
    {
        var applicantList = await _applicantRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, appl => appl.CreatedBy == _dbContext.GetCurrentUserId(), "Partner");
        var response = CustomMapper.Mapper.Map<SearchModel<ApplicantsResponseDTO>>(applicantList);

        return response;
    }
}