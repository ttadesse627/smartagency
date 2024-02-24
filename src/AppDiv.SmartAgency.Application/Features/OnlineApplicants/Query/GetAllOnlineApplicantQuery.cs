using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Query;
public class GetAllOnlineApplicantQuery : IRequest<SearchModel<OnlineApplicantResponseDTO>>
{

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
    public string? OrderBy { get; set; } = string.Empty;
    public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
    public GetAllOnlineApplicantQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        SortingDirection = sortingDirection;
    }

}

public class GetAllOnlineApplicantHandler : IRequestHandler<GetAllOnlineApplicantQuery, SearchModel<OnlineApplicantResponseDTO>>
{
    private readonly IOnlineApplicantRepository _onlineApplicantRepository;
    private readonly ISmartAgencyDbContext _dbContext;

    public GetAllOnlineApplicantHandler(IOnlineApplicantRepository onlineApplicantQueryRepository, ISmartAgencyDbContext dbContext)
    {
        _onlineApplicantRepository = onlineApplicantQueryRepository;
        _dbContext = dbContext;
    }
    public async Task<SearchModel<OnlineApplicantResponseDTO>> Handle(GetAllOnlineApplicantQuery request, CancellationToken cancellationToken)
    {

        var onlineApplicantList = await _onlineApplicantRepository.GetAllWithSearchAsync
                            (
                                request.SearchTerm!, null, ["MaritalStatus", "DesiredCountry", "Experience"]
                            );
        var paginatedOnlineApplicants = await _onlineApplicantRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, onlineApplicantList, request.OrderBy);
        var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<OnlineApplicantResponseDTO>>(paginatedOnlineApplicants);
        return paginatedListResp;
    }
}