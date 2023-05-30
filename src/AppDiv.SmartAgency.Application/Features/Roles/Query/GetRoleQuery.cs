using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.SmartAgency.Application.Features.Roles.Query
{
    public class GetRoleQuery : IRequest<IList<RoleResponseDTO>>
    {

    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IList<RoleResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRoleQueryHandler(IIdentityService identityService, RoleManager<IdentityRole> roleManager)
        {
            _identityService = identityService;
            _roleManager = roleManager;
        }
        public async Task<IList<RoleResponseDTO>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();
            var roleResponse = new List<RoleResponseDTO>();
            foreach (var role in roles)
            {
                var identityRole = await _roleManager.FindByIdAsync(role.id);
                if (identityRole == null)
                {
                    throw new NotFoundException("Role", role.id);
                }
                // Retrieve the claims associated with the role
                // var roleClaims = await _roleManager.GetClaimsAsync(identityRole);

                // // Map the claims to a list of strings
                // var resourceClaims = roleClaims
                //     .Where(c => c.Type == "Resource")
                //     .Select(c => c.Value)
                //     .ToList();

                roleResponse.Add(new RoleResponseDTO
                {
                    Id = Guid.Parse(identityRole.Id),
                    RoleName = identityRole.Name
                });
            }

            return roleResponse;
        }
    }
}