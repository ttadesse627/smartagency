
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Groups;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
namespace AppDiv.SmartAgency.Application.Features.Groups.Commands.Update
{
    public record GroupUpdateCommand(UpdateGroupRequest Group) : IRequest<ServiceResponse<int>> { }
    public class GroupUpdateCommandsHandler(IGroupRepository groupRepository) : IRequestHandler<GroupUpdateCommand, ServiceResponse<int>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<ServiceResponse<int>> Handle(GroupUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            var updatingGroup = await _groupRepository.GetWithPredicateAsync(ug => ug.Id == request.Group.Id, "Permissions");
            if (updatingGroup is not null)
            {
                updatingGroup.Name = request.Group.Name;
                updatingGroup.Permissions = CustomMapper.Mapper.Map<List<Permission>>(request.Group.Permissions);
                try
                {
                    response.Success = await _groupRepository.SaveChangesAsync(cancellationToken);
                    if (response.Success)
                    {
                        response.Message = "Successfully updated the group";
                        response.Data = 1;
                    }
                }
                catch (Exception exp)
                {
                    response.Errors?.Add(exp.Message);
                    throw new ApplicationException(exp.Message);
                }
            }
            return response;
        }
    }
}