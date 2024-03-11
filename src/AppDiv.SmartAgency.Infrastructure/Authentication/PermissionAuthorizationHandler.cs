using System.IdentityModel.Tokens.Jwt;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ClassScheduler.Infrastructure.Authentication;
public class PermissionAuthorizationHandler(IServiceScopeFactory serviceScope) : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScope = serviceScope;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }

        using IServiceScope serviceScope = _serviceScope.CreateScope();
        IPermissionRepository permissionRepository = serviceScope.ServiceProvider.GetRequiredService<IPermissionRepository>();
        HashSet<string> permissions = await permissionRepository.GetPermissionAsync(parsedUserId.ToString());

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}