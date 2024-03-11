
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Groups;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;
namespace AppDiv.SmartAgency.Application.Features.Groups.Commands.Create
{
    public record CreateGroupCommand(AddGroupRequest Group) : IRequest<ServiceResponse<int>> { }
    public class CreateGroupCommandHAndler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, ServiceResponse<int>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<ServiceResponse<int>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();

            // var validator = new CreateGroupComadValidetor(_groupRepository);
            // var validationResult = await validator.ValidateAsync(request, cancellationToken);

            // //Check and log validation errors
            // if (validationResult.Errors.Count > 0)
            // {
            //     response.Success = false;
            //     response.Errors = new List<string>();
            //     foreach (var error in validationResult.Errors)
            //         response.Errors.Add(error.ErrorMessage);
            //     response.Message = response.Errors[0];
            // }
            var permissions = new List<Permission>();
            if (request.Group.Permissions.Count != 0)
            {
                foreach (var permission in request.Group.Permissions)
                {
                    permissions.Add(new() { Name = permission.Name, Actions = permission.Actions });
                }
            }
            var group = new UserGroup
            {
                Name = request.Group.Name,
                Permissions = permissions
            };

            try
            {
                await _groupRepository.InsertAsync(group, cancellationToken);
                var result = await _groupRepository.SaveChangesAsync(cancellationToken);
                if (result)
                {
                    response.Data = Convert.ToInt32(result);
                    response.Success = true;
                    response.Message = "Created Successfully!";
                }
            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.Errors?.Add(ex.Message);
                throw new BadRequestException(ex.Message);
            }
            return response;
        }
    }
}