using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;

namespace AppDiv.SmartAgency.Application.Features.Users.Query
{
    public record GetUserDetailsByUserNameQuery(string username) : IRequest<UserDetailsResponseDTO> { }
    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService, IUserRepository usesrRepository)
        {
            _identityService = identityService;
            _userRepository = usesrRepository;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var expLoadedProps = new string[] { "Address", "UserGroups", "Position", "Branch", "Partner", "Address.Region" };

            var user = await _userRepository.GetWithPredicateAsync(user => user.UserName == request.username, expLoadedProps);
            var response = new UserDetailsResponseDTO();

            if (user.UserGroups == null || user.UserGroups.Count == 0)
            {
                // Position = user.Position.Value,
                //     Branch = user.Branch.Value,
                //     Partner = CustomMapper.Mapper.Map<UserPartnerResponseDTO>(user.Partner),
                response = new UserDetailsResponseDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Address = CustomMapper.Mapper.Map<AddressResponseDTO>(user.Address)
                };
            }else{
                response.Id= user.Id;
                response.FullName = user.FullName;
                response.UserName= user.UserName;
                response.Email = user.Email;
                response.Address = CustomMapper.Mapper.Map<AddressResponseDTO>(user.Address);
        
            }
            
            return response;
        }
    }
}