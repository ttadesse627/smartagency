using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.User.Query.GetUserById
{
    public record GetUserByIdQuery(Guid id) : IRequest<UserDetailsResponseDTO> { }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IIdentityService identityService, IUserRepository userRepository)
        {
            _identityService = identityService;
            _userRepository = userRepository;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var explLoadedProps = new string[] { "Partner", "Address.AddressRegion", "Address.Country", "UserGroups" };
            var user = await _userRepository.GetWithPredicateAsync(appl => appl.Id == request.id.ToString(), explLoadedProps);


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