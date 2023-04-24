
using AppDiv.SmartAgency.Application.Contracts.Request.LookUps;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.LookUps
{
    public record CreateLookUpCommand(CreateLookUpRequest lookUp) : IRequest<CreateLookUpCommandResponse>
{

}
}