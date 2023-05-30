using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? PartnerId { get; set; }
        public List<string>? Roles { get; set; } = null;
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
            if (request.Password != request.ConfirmationPassword)
            {
                throw new BadRequestException("The password should be confirmed");
            }
            var result = await _identityService.CreateUserAsync(
                request.UserName,
                request.Password,
                request.Email,
                request.FullName,
                request.Roles);
            return result.isSucceed ? 1 : 0;
        }
    }
}
