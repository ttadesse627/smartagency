
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
using System.Text;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

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

        var userResponse = new AuthResponseDTO();
        var explicitLoadedProperties = new String[] { "UserGroups", "Partner", "Partner.Orders", };
        var userData = await _userRepository.GetWithPredicateAsync(user => user.Id == response.userId!, explicitLoadedProperties);
        string token = _tokenGenerator.GenerateJWTToken((userData.Id, userData.UserName, response.roles)!);

        var userRoles = userData.UserGroups.SelectMany(ug => ug.Roles
            .Select(r => new RoleDto
            {
                Page = r.Value<string>("Page") ?? "",
                Title = r.Value<string>("Title") ?? "",
                CanAdd = r.Value<bool>("CanAdd"),
                CanDelete = r.Value<bool>("CanDelete"),
                CanViewDetail = r.Value<bool>("CanViewDetail"),
                CanView = r.Value<bool>("CanView"),
                CanUpdate = r.Value<bool>("CanUpdate")
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

        if (userData.Partner != null)
        {
            var words = userData.Partner.PartnerName.Split(' ');
            var abbrName = new StringBuilder();
            foreach (var word in words)
            {
                abbrName.Append(word.First());
            }
            var orderCount = userData.Partner.Orders?.Count + 1;
            var partResponse = new GetPartnerDropDownDTO
            {
                Id = userData.Partner.Id,
                PartnerName = userData.Partner.PartnerName,
                OrderNumber = abbrName.ToString() + " 00" + orderCount
            };
            userResponse.Partner = partResponse;
        }
        userResponse.UserId = userData.Id;
        userResponse.Username = userData.UserName;
        userResponse.Token = token;
        userResponse.FullName = userData.FullName!;
        userResponse.Roles = userRoles.ToList();

        return userResponse;
    }
}