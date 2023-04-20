using AppDiv.CRVS.Application.Exceptions;
using AppDiv.CRVS.Application.Contracts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.CRVS.Application.Interfaces;

namespace AppDiv.CRVS.Application.Features.Auth
{
    public class AuthCommand : IRequest<AuthResponseDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDTO>
    {
        private readonly ITokenGeneratorService _tokenGenerator;
        private readonly IIdentityService _identityService;

        public AuthCommandHandler(IIdentityService identityService, ITokenGeneratorService tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.AuthenticateUser(request.UserName, request.Password);

            if (!response.result.Succeeded)
            {
                throw new InvalidLoginException(string.Join(",", response.result.Errors));
            }

            // var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            string token = _tokenGenerator.GenerateJWTToken((response.user.Id, response.user.UserName, response.roles));

            return new AuthResponseDTO()
            {
                UserId = response.user.Id,
                Name = response.user.UserName,
                Token = token
            };
        }
    }
}