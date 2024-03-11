using Microsoft.AspNetCore.Authorization;

namespace ClassScheduler.Infrastructure.Authentication;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
