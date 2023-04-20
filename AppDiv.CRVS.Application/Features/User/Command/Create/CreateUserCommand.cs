using AppDiv.CRVS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Features.User.Command.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserGroupId { get; set; }
        public string PersonalInfoId { get; set;}
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.createUser(
                request.UserName,
                request.Email,
                request.UserGroupId,
                request.PersonalInfoId);
            return response.result.Succeeded ? 1 : 0;
        }
    }
}
