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
        if (context.User is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "No user found!"));
        }
        // Get the user's permissions from claims (assuming permissions are stored in claims)

        // Check if the user has the required permission
        var requiredPermission = await _groupRepository.CheckPermissionsAsync(userId, requirement.PermissionName, requirement.Action);
        if (requiredPermission)
        {
            context.Succeed(requirement);
        }
        else context.Fail(new AuthorizationFailureReason(this, "You're not authorized!"));

    }
}