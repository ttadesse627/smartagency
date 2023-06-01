// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AppDiv.CRVS.Application.Contracts.DTOs;
// using AppDiv.CRVS.Application.Features.Lookups.Query.GetAllUser;
// using AppDiv.CRVS.Application.Interfaces;
// using AppDiv.CRVS.Application.Mapper;
// using MediatR;

// namespace AppDiv.CRVS.Application.Features.User.Query
// {
//     public class GetUserDetailsByUserNameQuery : IRequest<UserResponseDTO>
//     {
//         public string UserName { get; set; }
//     }

//     public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserResponseDTO>
//     {
//         private readonly IIdentityService _identityService;

//         public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
//         {
//             _identityService = identityService;
//         }
//         public async Task<UserResponseDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
//         {
//             var user = await _identityService.GetUserByName(request.UserName);
//             return CustomMapper.Mapper.Map<UserResponseDTO>(user);
//             // { Id = userId, FullName = fullName, UserName = userName, Email = email, Roles = roles };
//         }
//     }
// }