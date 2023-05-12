using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.ApplicantsFollowupStatus
{
    public record CreateApplicantFollowupStatusCommand(CreateApplicantFollowupStatusRequest applicantFollowupStatus) : IRequest<CreateApplicantFollowupStatusCommandResponse>
{

}
}