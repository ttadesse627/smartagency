// using AppDiv.CRVS.Application.Contracts.DTOs;
// using AppDiv.CRVS.Application.Features.Groups.Commands.Create;
// using AppDiv.CRVS.Application.Interfaces.Persistence;
// using AppDiv.CRVS.Application.Mapper;
// using AppDiv.CRVS.Domain.Entities;
// using AppDiv.CRVS.Domain.Repositories;
// using MediatR;
// using Newtonsoft.Json.Linq;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace AppDiv.CRVS.Application.Features.Lookups.Command.Update
// {


//     public class GroupUpdateCommandsHandler : IRequestHandler<GroupUpdateCommand, GroupDTO>
//     {
//         private readonly IGroupRepository _groupRepository;
//         public GroupUpdateCommandsHandler(IGroupRepository groupRepository)
//         {
//             _groupRepository = groupRepository;
//         }
//         public async Task<GroupDTO> Handle(GroupUpdateCommand request, CancellationToken cancellationToken)
//         {
//             // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request);
//             UserGroup groupEntity = new UserGroup
//             {
//                 Id = request.group.Id,
//                 GroupName = request.group.GroupName,
//                 Description = request.group.Description,
//                 Roles = request.group.Roles,
//             };
//             try
//             {
//                 await _groupRepository.UpdateAsync(groupEntity, x => x.Id);
//                 await _groupRepository.SaveChangesAsync(cancellationToken);
//             }
//             catch (Exception exp)
//             {
//                 throw new ApplicationException(exp.Message);
//             }
//             var modifiedLookup = await _groupRepository.GetAsync(request.group.Id);
//             var LookupResponse = CustomMapper.Mapper.Map<GroupDTO>(modifiedLookup);
//             return LookupResponse;
//         }
//     }
// }