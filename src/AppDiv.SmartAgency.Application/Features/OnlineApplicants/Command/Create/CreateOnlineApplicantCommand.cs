
using AppDiv.SmartAgency.Application.Contracts.Request.OnlineApplicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Create
{
    public record CreateOnlineApplicantCommand(OnlineApplicantRequest onlineApplicant) : IRequest<CreateOnlineApplicantCommandResponse>
    {

    }
}