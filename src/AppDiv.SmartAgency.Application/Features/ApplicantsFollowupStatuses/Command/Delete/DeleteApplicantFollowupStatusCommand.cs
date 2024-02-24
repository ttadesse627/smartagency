using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Delete
{
    public record DeleteApplicantFollowupStatusCommand(List<Guid> Ids) : IRequest<String>
    {

    }

    public class DeleteApplicantFollowupStatusCommmandHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository) : IRequestHandler<DeleteApplicantFollowupStatusCommand, String>
    {
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository = applicantFollowupStatusRepository;

        public async Task<string> Handle(DeleteApplicantFollowupStatusCommand request, CancellationToken cancellationToken)
        {
            int response;
            try
            {
                response = await _applicantFollowupStatusRepository.DeleteMany(request.Ids);

            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            return response + " Applicant followup status information have been deleted!";
        }
    }
}