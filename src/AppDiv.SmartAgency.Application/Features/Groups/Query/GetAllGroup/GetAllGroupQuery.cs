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
            var userGroups = _groupRepository.GetMultipleUserGroupsBySearch(request.SearchTerm);
            var paginatedUsers = await _groupRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, userGroups, request.OrderBy);
            var groupResponse = paginatedUsers.Items.Select(ug => new FetchGroupDTO
            {
                Id = ug.Id,
                Name = ug.Name,
                Permissions = ug.Permissions.Select(p => new PermissionDto
                {
                    Id = p.Id,
                    Resource = new ResourceResponseDTO { Id = p.Resource.Id, Name = p.Resource.Name },
                    Actions = p.Actions.Select(ac => ac.ToString()).ToList()
                }).ToList()
            });
            return new SearchModel<FetchGroupDTO>
            {
                Items = groupResponse,
                CurrentPage = paginatedUsers.CurrentPage,
                MaxPage = paginatedUsers.MaxPage,
                TotalCount = paginatedUsers.TotalCount,
                SearchKeyWord = paginatedUsers.SearchKeyWord,
                PagingSize = paginatedUsers.PagingSize,
                Filters = paginatedUsers.Filters,
                ObjectFilters = paginatedUsers.ObjectFilters,
                SortingColumn = paginatedUsers.SortingColumn,
                SortingDirection = paginatedUsers.SortingDirection,
                Tags = paginatedUsers.Tags,

            };
        }
    }
}