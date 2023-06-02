using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Create
{
    public record CreateUserCommand(AddUserRequest request) : IRequest<int> { }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.request;
            if (request.Password != request.ConfirmationPassword)
            {
                throw new BadRequestException("The password should be confirmed");
            }
        //     var result = await _identityService.CreateUserAsync(
        //         request.UserName,
        //         request.Password,
        //         request.Email,
        //         request.FullName,);
        //     return result.isSucceed ? 1 : 0;
        return 1;
        }
    }
}
