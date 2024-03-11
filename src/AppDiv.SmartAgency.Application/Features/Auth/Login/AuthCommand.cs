
using MediatR;
using Microsoft.Extensions.Logging;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using System.Security.Authentication;
using System.Text;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;

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
        var (result, roles, userId) = await _identityService.AuthenticateUser(request.UserName, request.Password);

        if (!result.Succeeded)
        {
            throw new AuthenticationException(string.Join(",", result.Errors));
        }

        var userResponse = new AuthResponseDTO();
        var explicitLoadedProperties = new String[] { "UserGroups", "Partner", "Partner.Orders", };
        var userData = await _userRepository.GetWithPredicateAsync(user => user.Id == userId!, explicitLoadedProperties);
        string token = _tokenGenerator.GenerateJWTToken((userData.Id, userData.UserName, roles)!);

        var userRoles = userData.UserGroups.SelectMany(ug => ug.Permissions
                                 .Select(r => new PermissionDto
                                 {
                                     Name = r.Name,
                                     Actions = r.Actions.Select(ac => ac.ToString()).ToList()
                                 })).GroupBy(r => r.Name.Trim(), StringComparer.OrdinalIgnoreCase).Select(g => new PermissionDto
                                 {
                                     Name = g.Key,
                                     Actions = g.SelectMany(p => p.Actions).ToList()
                                 }).ToList();

        if (userData.Partner != null)
        {
            var words = userData.Partner.PartnerName?.Split(' ');
            var abbrName = new StringBuilder();
            foreach (var word in words!)
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
        userResponse.Username = userData.UserName!;
        userResponse.Token = token;
        userResponse.FullName = userData.FullName!;
        userResponse.Permissions = [.. userRoles];

        return userResponse;
    }
}