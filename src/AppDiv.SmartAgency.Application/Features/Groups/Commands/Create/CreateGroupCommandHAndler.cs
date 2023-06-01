// using System.Linq;
// using AppDiv.CRVS.Application.Exceptions;
// using AppDiv.CRVS.Application.Contracts.DTOs;
// using AppDiv.CRVS.Application.Mapper;
// using AppDiv.CRVS.Domain.Entities;
// using AppDiv.CRVS.Domain.Repositories;
// using MediatR;
// using ApplicationException = AppDiv.CRVS.Application.Exceptions.ApplicationException;
// using AppDiv.CRVS.Application.Interfaces.Persistence;

// namespace AppDiv.CRVS.Application.Features.Groups.Commands.Create
// {

//     public class CreateGroupCommandHAndler : IRequestHandler<CreateGroupCommand, CreateGroupComandResponse>
//     {
//         private readonly IGroupRepository _groupRepository;
//         public CreateGroupCommandHAndler(IGroupRepository groupRepository)
//         {
//             _groupRepository = groupRepository;
//         }
//         public async Task<CreateGroupComandResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
//         {

//             // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

//             var CreateGroupComandResponse = new CreateGroupComandResponse();

//             var validator = new CreateGroupComadValidetor(_groupRepository);
//             var validationResult = await validator.ValidateAsync(request, cancellationToken);

//             //Check and log validation errors
//             if (validationResult.Errors.Count > 0)
//             {
//                 CreateGroupComandResponse.Success = false;
//                 CreateGroupComandResponse.ValidationErrors = new List<string>();
//                 foreach (var error in validationResult.Errors)
//                     CreateGroupComandResponse.ValidationErrors.Add(error.ErrorMessage);
//                 CreateGroupComandResponse.Message = CreateGroupComandResponse.ValidationErrors[0];
//             }
//             if (CreateGroupComandResponse.Success)
//             {
//                 //can use this instead of automapper
//                 var group = new UserGroup
//                 {
//                     Id = Guid.NewGuid(),
//                     GroupName = request.group.GroupName,
//                     Description = request.group.Description,
//                     Roles = request.group.Roles
//                 };
//                 //
//                 await _groupRepository.InsertAsync(group, cancellationToken);
//                 var result = await _groupRepository.SaveChangesAsync(cancellationToken);

//                 //var customerResponse = CustomerMapper.Mapper.Map<CustomerResponseDTO>(customer);
//                 // CreateGroupComandResponse.Customer = customerResponse;          
//             }
//             return CreateGroupComandResponse;
//         }
//     }
// }
