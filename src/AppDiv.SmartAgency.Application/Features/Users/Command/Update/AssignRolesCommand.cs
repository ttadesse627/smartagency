using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.User.Command.Update;
public record AssignRolesCommand(AssignRolesRequest Request) : IRequest<ServiceResponse<int>> { }

public class AssignRolesCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository) : IRequestHandler<AssignRolesCommand, ServiceResponse<int>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ServiceResponse<int>> Handle(AssignRolesCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        ServiceResponse<int> response = new();
        ICollection<UserGroup> listGroup = [];
        if (request.GroupIds.Count > 0)
        {
            listGroup = await _groupRepository.GetMultipleUserGroups(request.GroupIds);
        }
        var user = await _userRepository.GetWithPredicateAsync(user => user.Id == request.UserId.ToString(), "UserGroups");
        if (user != null)
        {
            foreach (var group in listGroup)
            {
                user.UserGroups.Add(group);
            }
            await _userRepository.UpdateAsync(user, user => user.Id);
            try
            {
                response.Success = await _userRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Message = "The user is successfully Updated!";
                    response.Data = 1;
                }

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
        }
        return response;
    }
}
