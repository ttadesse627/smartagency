using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class HasPermissionAttribute(PermissionEnum action) : AuthorizeAttribute(policy: action.ToString()) { }
