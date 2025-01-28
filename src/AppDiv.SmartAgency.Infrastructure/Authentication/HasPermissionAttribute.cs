using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string controllerName, PermissionEnum permissionAction)
    {
        Policy = $"{controllerName}.{permissionAction}";
    }
}
