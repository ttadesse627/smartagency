
using AppDiv.SmartAgency.Application.Contracts.Request.LookUps;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Create
{
    public record CreateLookUpCommand(CreateLookUpRequest lookUp) : IRequest<CreateLookUpCommandResponse>
{
}
}