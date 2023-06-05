
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroups

{
    // Customer query with List<Customer> response
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

            var groups = await _groupRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection);
            var groupResponse = CustomMapper.Mapper.Map<SearchModel<FetchGroupDTO>>(groups);
            return groupResponse;
        }
    }
}