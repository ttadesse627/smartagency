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
        var expLoadedProps = new string[] { "MaritalStatus", "Religion", "BrokerName" };
        var applicantList = await _applicantRepository.GetAllWithSearchAsync(
            request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection,
            null, expLoadedProps);

        var response = CustomMapper.Mapper.Map<SearchModel<ApplicantsResponseDTO>>(applicantList);
        var itemsArray = response.Items.ToArray();
        var entitiesArray = applicantList.Items.ToArray();
        for (var i = 0; i < itemsArray.Length; i++)
        {
            for (var j = 0; j < entitiesArray.Length; j++)
            {
                if (i == j)
                {
                    itemsArray[i].MaritalStatus = entitiesArray[j].MaritalStatus?.Value!;
                    itemsArray[i].Religion = entitiesArray[j].Religion?.Value;
                    itemsArray[i].BrokerName = entitiesArray[j].BrokerName?.Value;
                    itemsArray[i].RegisteredDate = entitiesArray[j].CreatedAt;
                    itemsArray[i].Gender = entitiesArray[j].Gender;
                }
                if (i < j) break;
            }
        }
        response.Items = itemsArray.AsEnumerable();
        response.CurrentPage = applicantList.CurrentPage;
        response.Filters = applicantList.Filters;
        response.MaxPage = applicantList.MaxPage;
        response.PagingSize = applicantList.PagingSize;
        response.SearchKeyWord = applicantList.SearchKeyWord;
        response.SortingDirection = applicantList.SortingDirection;
        response.SortingColumn = applicantList.SortingColumn;
        response.TotalCount = applicantList.TotalCount;

        return response;
    }
}