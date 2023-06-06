using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;

namespace AppDiv.SmartAgency.Application.Features.Users.Query
{
    public record GetUserDetailsByUserNameQuery(string username) : IRequest<UserDetailsResponseDTO> { }
    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserDetailsByUserNameAsync(request.username);
            var response = new UserDetailsResponseDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Position = user.Position.Value,
                Branch = user.Branch.Value,
                Partner = CustomMapper.Mapper.Map<UserPartnerResponseDTO>(user.Partner),
                Address = CustomMapper.Mapper.Map<AddressResponseDTO>(user.Address),
                UserGroups = CustomMapper.Mapper.Map<List<GroupDTO>>(user.UserGroups)
            };
            return response;
        }
    }
}