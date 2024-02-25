using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Queries;
public class GetAllApplicants : IRequest<SearchModel<ApplicantsResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public string? OrderBy { get; set; }
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

    public GetAllApplicantsHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<SearchModel<ApplicantsResponseDTO>> Handle(GetAllApplicants request, CancellationToken cancellationToken)
    {
        var expLoadedProps = new string[] { "MaritalStatus", "Religion", "BrokerName" };
        var applicantList = await _applicantRepository.GetAllWithSearchAsync(
            request.SearchTerm, null, expLoadedProps);

        var paginatedApplicants = await _applicantRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, applicantList, request.OrderBy);

        var response = CustomMapper.Mapper.Map<SearchModel<ApplicantsResponseDTO>>(paginatedApplicants);
        var itemsArray = response.Items.ToArray();
        var entitiesArray = paginatedApplicants.Items.ToArray();
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
        response.CurrentPage = paginatedApplicants.CurrentPage;
        response.Filters = paginatedApplicants.Filters;
        response.MaxPage = paginatedApplicants.MaxPage;
        response.PagingSize = paginatedApplicants.PagingSize;
        response.SearchKeyWord = paginatedApplicants.SearchKeyWord;
        response.SortingDirection = paginatedApplicants.SortingDirection;
        response.SortingColumn = paginatedApplicants.SortingColumn;
        response.TotalCount = paginatedApplicants.TotalCount;

        return response;
    }
}