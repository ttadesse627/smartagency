
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

            var permissions = request.Group.Permissions.Select(permission =>
                new Permission { ResourceId = permission.ResourceId, Actions = permission.Actions }).ToList();

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