using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Roles.Command.Create
{
    public class RoleCreateCommand : IRequest<int>
    {
        public IList<string> RoleNames { get; set; }
    }

    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, int>
    {
        private readonly IIdentityService _identityService;

        public RoleCreateCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateRoleAsync(request.RoleNames);
            return result ? 1 : 0;
        }
    }
}