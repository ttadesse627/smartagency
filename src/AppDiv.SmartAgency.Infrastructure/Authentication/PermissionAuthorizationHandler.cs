using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;
public class PermissionAuthorizationHandler(string controllerName, PermissionEnum controllerAction, IUserRepository userRepository) : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _controllerName = controllerName;
    private readonly PermissionEnum _controllerAction = controllerAction;
    private readonly IUserRepository _userRepository = userRepository;
    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        string? userId = context.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (userId is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (await _userRepository.PermissionExists(userId, _controllerName)) return;
        context.Result = new ForbidResult("UnAuthorized Access!");
        return;
    }
}