using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroups

{
    public class GetAllGroupQuery(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection) : IRequest<SearchModel<FetchGroupDTO>>
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public string SearchTerm { get; set; } = searchTerm;
        public string OrderBy { get; set; } = orderBy;
        public SortingDirection SortingDirection { get; set; } = sortingDirection;
    }

    public class GetAllGroupQueryHandler(IGroupRepository groupRepository) : IRequestHandler<GetAllGroupQuery, SearchModel<FetchGroupDTO>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<SearchModel<FetchGroupDTO>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
        {
            var userGroups = await _groupRepository.GetAllWithPredicateSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, null, "Permissions.Resource");
            // var userGroups = await _groupRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, userGroups, request.OrderBy);
            var groupResponse = userGroups.Items.Select(ug => new FetchGroupDTO
            {
                Id = ug.Id,
                Name = ug.Name,
                Permissions = [.. ug.Permissions.Select(p => new PermissionDto
                {
                    Id = p.Id,
                    Resource = new ResourceResponseDTO { Id = p.Resource.Id, Name = p.Resource.Name },
                    Actions = p.Actions
                })]
            });
            return new SearchModel<FetchGroupDTO>
            {
                Items = groupResponse,
                CurrentPage = userGroups.CurrentPage,
                MaxPage = userGroups.MaxPage,
                TotalCount = userGroups.TotalCount,
                SearchKeyWord = userGroups.SearchKeyWord,
                PagingSize = userGroups.PagingSize,
                Filters = userGroups.Filters,
                ObjectFilters = userGroups.ObjectFilters,
                SortingColumn = userGroups.SortingColumn,
                SortingDirection = userGroups.SortingDirection,
                Tags = userGroups.Tags,
            };
        }
    }
}