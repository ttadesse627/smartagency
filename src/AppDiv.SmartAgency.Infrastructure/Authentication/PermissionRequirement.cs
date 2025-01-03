
using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class PermissionRequirement(string permissionName, PermissionEnum action) : IAuthorizationRequirement
{
    public string PermissionName { get; } = permissionName;
    public PermissionEnum Action { get; } = action;
}