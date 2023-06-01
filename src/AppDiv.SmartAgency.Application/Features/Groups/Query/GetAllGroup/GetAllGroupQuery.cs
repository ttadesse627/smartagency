// using AppDiv.CRVS.Application.Common;
// using AppDiv.CRVS.Application.Contracts.DTOs;
// using AppDiv.CRVS.Application.Interfaces.Persistence;
// using AppDiv.CRVS.Application.Mapper;
// using AppDiv.CRVS.Domain.Entities;
// using AppDiv.CRVS.Domain.Repositories;
// using AppDiv.SmartAgency.Utility.Contracts;
// using MediatR;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetAllGroups

// {
//     // Customer query with List<Customer> response
//     public record GetAllGroupQuery : IRequest<SearchModel<FetchGroupDTO>>
//     {
//         public int? PageCount { set; get; } = 1!;
//         public int? PageSize { get; set; } = 10!;
//     }

//     public class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupQuery, SearchModel<FetchGroupDTO>>
//     {
//         private readonly IGroupRepository _groupRepository;

//         public GetAllGroupQueryHandler(IGroupRepository groupRepository)
//         {
//             _groupRepository = groupRepository;
//         }
//         public async Task<SearchModel<FetchGroupDTO>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
//         {
//             var grouplist = _groupRepository.GetAll();
//             return await SearchModel<FetchGroupDTO>
//                             .CreateAsync(
//                                 _groupRepository.GetAll().Select(g => new FetchGroupDTO
//                                 {
//                                     Id = g.Id,
//                                     GroupName = g.GroupName,
//                                     Description = g.Description.Value<string>("eng")
//                                 }).ToList()

//                                 , request.PageCount ?? 1, request.PageSize ?? 10);
//         }
//     }
// }