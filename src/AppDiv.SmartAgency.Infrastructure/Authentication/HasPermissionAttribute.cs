using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class HasPermissionAttribute : TypeFilterAttribute
{
    public HasPermissionAttribute(string controllerName, PermissionEnum permissionAction) : base(typeof(PermissionAuthorizationHandler))
    {
        Arguments = [new PermissionRequirement(controllerName, permissionAction)];
    }
}
