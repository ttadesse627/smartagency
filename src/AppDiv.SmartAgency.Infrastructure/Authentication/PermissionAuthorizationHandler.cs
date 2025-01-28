using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserRepository _userRepository;
    public PermissionAuthorizationHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        // Get the user's permissions from claims (assuming permissions are stored in claims)
        var userPermissions = context.User.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        // Check if the user has the required permission
        var requiredPermission = $"{requirement.PermissionName}.{requirement.Action}";
        if (userPermissions.Contains(requiredPermission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}