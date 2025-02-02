using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using System.Security.Authentication;
using System.Text;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;

namespace AppDiv.SmartAgency.Application.Features.Auth.Login;
public class AuthCommand : IRequest<AuthResponseDTO>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

public class AuthCommandHandler(IIdentityService identityService, ITokenGeneratorService tokenGenerator, IUserRepository userRepository) : IRequestHandler<AuthCommand, AuthResponseDTO>
{
    private readonly ITokenGeneratorService _tokenGenerator = tokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IIdentityService _identityService = identityService;

    public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var (result, roles, userId) = await _identityService.AuthenticateUser(request.UserName, request.Password);

        if (!result.Succeeded)
        {
            throw new AuthenticationException(string.Join(",", result.Errors));
        }

        var userResponse = new AuthResponseDTO();
        var explicitLoadedProperties = new string[] { "UserGroups", "UserGroups.Permissions", "UserGroups.Permissions.Resource", "Partner", "Partner.Orders", };
        var userData = await _userRepository.GetWithPredicateAsync(user => user.Id == userId!, explicitLoadedProperties);

        var userRoles = userData.UserGroups.SelectMany(ug => ug.Permissions
                                 .Select(r => new PermissionDto
                                 {
                                     Resource = new ResourceResponseDTO
                                     {
                                         Id = r.Resource.Id,
                                         Name = r.Resource.Name
                                     },
                                     Actions = r.Actions
                                 })).GroupBy(r => r?.Resource.Name?.Trim(), StringComparer.OrdinalIgnoreCase).Select(g => new PermissionDto
                                 {
                                     Resource = new ResourceResponseDTO { Id = g.First().Resource.Id, Name = g.Key },
                                     Actions = g.SelectMany(p => p.Actions).ToList()
                                 }).ToList();
        // IList<string?> permissions = [.. userRoles.Select(perm => perm.Resource.Name)];
        IList<string?> permissions = [.. userRoles.SelectMany(p => p.Actions.Select(ac => $"{p.Resource.Name}.{ac}"))];
        string token = _tokenGenerator.GenerateJWTToken((userData.Id, userData.UserName, permissions)!);

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