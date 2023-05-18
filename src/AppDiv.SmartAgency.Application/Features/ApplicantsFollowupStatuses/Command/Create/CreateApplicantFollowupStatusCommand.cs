using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.ApplicantFollowupStatuses;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Create
{
    public record CreateApplicantFollowupStatusCommand(CreateApplicantFollowupStatusRequest applicantFollowupStatus) : IRequest<CreateApplicantFollowupStatusCommandResponse>
{

}
}