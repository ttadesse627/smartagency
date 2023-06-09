using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Delete
{
    public record DeleteOnlineApplicantCommand(List<Guid> Ids): IRequest<String>
    {
        
    }

   
    // lookUp delete command handler with string response as output
    public class DeleteOnlineApplicantCommmandHandler : IRequestHandler<DeleteOnlineApplicantCommand, String>
    {
        private readonly IOnlineApplicantRepository _onlineApplicantRepository;
        
        public DeleteOnlineApplicantCommmandHandler( IOnlineApplicantRepository onlineApplicantRepository)
        {
            _onlineApplicantRepository= onlineApplicantRepository;
            
        }

        public async Task<string> Handle(DeleteOnlineApplicantCommand request, CancellationToken cancellationToken)
        {
             int response= 0;
            try
            {
                response = await _onlineApplicantRepository.DeleteMany(request.Ids);
         

            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return response + " Online Applicant information has been deleted!";
        }
    }
}