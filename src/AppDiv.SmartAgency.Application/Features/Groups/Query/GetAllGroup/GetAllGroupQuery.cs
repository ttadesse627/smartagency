
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroups

{
    public class GetAllGroupQuery : IRequest<SearchModel<FetchGroupDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllGroupQuery(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

    public class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupQuery, SearchModel<FetchGroupDTO>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetAllGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<SearchModel<FetchGroupDTO>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
        {
            var userGroups = await _groupRepository.GetAllWithSearchAsync(request.SearchTerm!, gr => gr.Id != null);
            var paginatedUsers = await _groupRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, userGroups, request.OrderBy);
            var groupResponse = CustomMapper.Mapper.Map<SearchModel<FetchGroupDTO>>(paginatedUsers);
            return groupResponse;
        }
    }
}