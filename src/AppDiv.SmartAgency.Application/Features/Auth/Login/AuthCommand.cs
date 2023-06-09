
using MediatR;
using Microsoft.Extensions.Logging;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using System.Security.Authentication;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Contracts.DTOs.RoleDTOs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Application.Features.Auth.Login;
public class AuthCommand : IRequest<AuthResponseDTO>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDTO>
{
    private readonly ITokenGeneratorService _tokenGenerator;
    private readonly ILogger<AuthCommandHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityService _identityService;

    public AuthCommandHandler(IIdentityService identityService, ITokenGeneratorService tokenGenerator, ILogger<AuthCommandHandler> logger, IUserRepository userRepository)
    {
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
    {

        var response = await _identityService.AuthenticateUser(request.UserName, request.Password);

        if (!response.result.Succeeded)
        {
            throw new AuthenticationException(string.Join(",", response.result.Errors));
        }
        var explicitLoadedProperties = new Dictionary<string, Utility.Contracts.NavigationPropertyType>
                                                {
                                                    { "UserGroups", NavigationPropertyType.COLLECTION }
                                                };
        var userData = await _userRepository.GetWithAsync(response.userId!, explicitLoadedProperties);
        string token = _tokenGenerator.GenerateJWTToken((userData.Id, userData.UserName, response.roles)!);

        var userRoles = userData.UserGroups.SelectMany(ug => ug.Roles
        .Select(r => JToken.Parse(JsonConvert.SerializeObject(r)).Value<string>("Page") ?? "")
        .Select(page => new RoleDto
        {
            Page = page,
            Title = JToken.Parse(page).Value<string>("Title") ?? "",
            CanAdd = JToken.Parse(page).Value<bool>("CanAdd"),
            CanDelete = JToken.Parse(page).Value<bool>("CanDelete"),
            CanViewDetail = JToken.Parse(page).Value<bool>("CanViewDetail"),
            CanView = JToken.Parse(page).Value<bool>("CanView"),
            CanUpdate = JToken.Parse(page).Value<bool>("CanUpdate")
        })).GroupBy(r => r.Page.Trim(), StringComparer.OrdinalIgnoreCase).Select(g => new RoleDto
        {
            Page = g.Key,
            Title = g.FirstOrDefault()?.Title ?? "",
            CanAdd = g.Aggregate(false, (acc, x) => acc || x.CanAdd),
            CanDelete = g.Aggregate(false, (acc, x) => acc || x.CanDelete),
            CanUpdate = g.Aggregate(false, (acc, x) => acc || x.CanUpdate),
            CanView = g.Aggregate(false, (acc, x) => acc || x.CanView),
            CanViewDetail = g.Aggregate(false, (acc, x) => acc || x.CanViewDetail)
        });
        return new AuthResponseDTO()
        {
            UserId = userData.Id,
            Username = userData.UserName,
            Token = token,
            FullName = userData.FullName!,
            Roles = userRoles.ToList(),
        };
    }
}