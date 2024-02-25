
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
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
            var userGroups = await _groupRepository.GetAllWithSearchAsync(request.SearchTerm, gr => gr.Id != null);
            var paginatedUsers = await _groupRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, userGroups, request.OrderBy);
            var groupResponse = CustomMapper.Mapper.Map<SearchModel<FetchGroupDTO>>(paginatedUsers);
            return groupResponse;
        }
    }
}