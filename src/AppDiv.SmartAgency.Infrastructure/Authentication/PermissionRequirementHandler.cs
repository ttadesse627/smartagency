

using System.Security.Claims;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class PermissionRequirementHandler(IUserRepository userRepository) : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserRepository _userRepository = userRepository;
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            context.Fail();
        }

        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            context.Fail();
        }

        foreach (var group in user.UserGroups)
        {
            foreach (var permission in group.Permissions)
            {
                if (permission.Name == requirement.PermissionName && permission.Actions.Contains(requirement.Action))
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}