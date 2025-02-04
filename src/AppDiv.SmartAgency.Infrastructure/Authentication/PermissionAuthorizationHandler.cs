using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IGroupRepository _groupRepository;
    public PermissionAuthorizationHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.Claims.Where(cl => cl.Type == "UserId").FirstOrDefault()?.Value;
        string? groupIds = context.User.Claims.Where(claim => claim.Type == "UserGroupIds").FirstOrDefault()?.Value;
        if (userId is null || string.IsNullOrEmpty(groupIds))
        {
            context.Fail(new AuthorizationFailureReason(this, "No user found!"));
        }
        else
        {
            var requiredPermission = await _groupRepository.CheckPermissionsAsync(groupIds, requirement.PermissionName, requirement.Action);
            if (requiredPermission)
            {
                context.Succeed(requirement);
            }
            else context.Fail(new AuthorizationFailureReason(this, "You're not authorized!"));
        }

    }
}