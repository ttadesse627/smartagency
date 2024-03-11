
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Groups;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
namespace AppDiv.SmartAgency.Application.Features.Groups.Commands.Update
{
    public record GroupUpdateCommand(UpdateGroupRequest group) : IRequest<ServiceResponse<int>> { }
    public class GroupUpdateCommandsHandler : IRequestHandler<GroupUpdateCommand, ServiceResponse<int>>
    {
        private readonly IGroupRepository _groupRepository;
        public GroupUpdateCommandsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<ServiceResponse<int>> Handle(GroupUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            var groupEntity = new UserGroup
            {
                Id = request.group.Id,
                Name = request.group.Name,
                Permissions = CustomMapper.Mapper.Map<List<Permission>>(request.group.Permissions),
            };
            try
            {
                await _groupRepository.UpdateAsync(groupEntity, x => x.Id);
                response.Success = await _groupRepository.SaveChangesAsync(cancellationToken);

            }
            catch (Exception exp)
            {
                response.Errors?.Add(exp.Message);
                throw new ApplicationException(exp.Message);
            }
            return response;
        }
    }
}