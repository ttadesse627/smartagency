using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Delete
{
    public record DeleteApplicantFollowupStatusCommand(List<Guid> Ids): IRequest<String>
    {
       
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
            int response = 0;
            try
            {
              response= await _applicantFollowupStatusRepository.DeleteMany(request.Ids);                   

            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return response + " Applicant followup status information have been deleted!";
        }
    }
}