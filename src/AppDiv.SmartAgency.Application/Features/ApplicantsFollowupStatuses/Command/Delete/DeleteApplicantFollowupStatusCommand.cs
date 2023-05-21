using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Delete
{
    public class DeleteApplicantFollowupStatusCommand: IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeleteApplicantFollowupStatusCommand(Guid Id)
        {
            this.Id = Id;
        }
    }

   
    // lookUp delete command handler with string response as output
    public class DeleteApplicantFollowupStatusCommmandHandler : IRequestHandler<DeleteApplicantFollowupStatusCommand, String>
    {
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;
        
        public DeleteApplicantFollowupStatusCommmandHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository)
        {
            _applicantFollowupStatusRepository= applicantFollowupStatusRepository;
          
        }

        public async Task<string> Handle(DeleteApplicantFollowupStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var applicantFollowupStatusEntity = await _applicantFollowupStatusRepository.GetByIdAsync(request.Id);
                await _applicantFollowupStatusRepository.DeleteAsync(applicantFollowupStatusEntity.Id);
                 await _applicantFollowupStatusRepository.SaveChangesAsync(cancellationToken);
                

            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Applicant followup status information has been deleted!";
        }
    }
}