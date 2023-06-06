
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var eagerLoadedProperties = new string[] { "UserGroups" };

            var userList = await _userRepository.GetAllWithSearchAsync
                            (
                                request.PageNumber, request.PageSize, request.SearchTerm!, request.OrderBy!,
                                request.SortingDirection, user => user.UserName != null, eagerLoadedProperties
                            );
            var userResponse = CustomMapper.Mapper.Map<SearchModel<UserResponseDTO>>(userList);
            return userResponse;
        }
    }
}