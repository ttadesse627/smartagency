using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Delete
{
    public class DeleteOnlineApplicantCommand: IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeleteOnlineApplicantCommand(Guid Id)
        {
            this.Id = Id;
        }
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
            try
            {
                var onlineApplicantEntity = await _onlineApplicantRepository.GetByIdAsync(request.Id);
                
                await _onlineApplicantRepository.DeleteAsync(onlineApplicantEntity.Id);
                
                 await _onlineApplicantRepository.SaveChangesAsync(cancellationToken);
         

            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Online Applicant information has been deleted!";
        }
    }
}