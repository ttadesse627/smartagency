
// using AppDiv.CRVS.Application.Contracts.DTOs;
// using AppDiv.CRVS.Application.Features.Groups.Query.GetAllGroup;
// using AppDiv.CRVS.Application.Features.Lookups.Query.GetAllLookup;
// using AppDiv.CRVS.Application.Interfaces.Persistence;
// using AppDiv.CRVS.Application.Mapper;
// using AppDiv.CRVS.Domain.Entities;
// using MediatR;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace AppDiv.CRVS.Application.Features.Groups.Query.GetGroupById
// {
//     // Customer GetGroupbyId with  response
//     public class GetGroupbyId : IRequest<GroupDTO>
//     {
//         public Guid Id { get; private set; }

//         public GetGroupbyId(Guid Id)
//         {
//             this.Id = Id;
//         }

//     }

//     public class GetGroupbyIdHandler : IRequestHandler<GetGroupbyId, GroupDTO>
//     {
//         private readonly IGroupRepository _groupRepository;

//         public GetGroupbyIdHandler(IGroupRepository groupRepository)
//         {

//             _groupRepository = groupRepository;
//         }
//         public async Task<GroupDTO> Handle(GetGroupbyId request, CancellationToken cancellationToken)
//         {
//             // var groups = await _mediator.Send(new GetAllGroupQuery());
//             var selectedGroup = await _groupRepository.GetAsync(request.Id);
//             return CustomMapper.Mapper.Map<GroupDTO>(selectedGroup);
//             // return selectedCustomer;
//         }
//     }
// }