using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Query
{
    public class GetUserDetailsByUserNameQuery : IRequest<UserDetailsResponseDTO>
    {
        public string UserName { get; set; }
    }

    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserDetailsResponseDTO() { Id = Guid.Parse(userId), FullName = fullName, UserName = userName, Email = email, Roles = roles };
        }
    }
}