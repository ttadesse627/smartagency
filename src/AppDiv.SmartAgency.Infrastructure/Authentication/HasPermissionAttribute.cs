using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class HasPermissionAttribute(Permission permission) : AuthorizeAttribute(policy: permission.ToString()) { }
