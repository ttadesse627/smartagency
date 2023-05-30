using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Roles.Command.Delete
{
    public class DeleteRoleCommand : IRequest<int>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, int>
    {
        private readonly IIdentityService _identityService;

        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteRoleAsync(request.RoleId);
            return result ? 1 : 0;
        }
    }
}