using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Lookups.Query.GetAllUser

{
    public record GetAllUserQuery : IRequest<SearchModel<UserResponseDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllUserQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, SearchModel<UserResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IIdentityService identityService, IUserRepository userRepository)
        {
            _identityService = identityService;
            _userRepository = userRepository;
        }
        public async Task<SearchModel<UserResponseDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {

            var userList = await _userRepository.GetAllWithSearchAsync
                            (
                                request.SearchTerm!, user => user.UserName != null, ["UserGroups", "UserGroups.Permissions"]
                            );
            var paginatedUsers = await _userRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, userList, request.OrderBy);
            var userResponse = CustomMapper.Mapper.Map<SearchModel<UserResponseDTO>>(paginatedUsers);
            return userResponse;
        }
    }
}