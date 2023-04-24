using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Delete.LookUps
{
    public class DeletePartnerCommand: IRequest<String>
    {
        public string Id { get; private set; }

        public DeletePartnerCommand(string Id)
        {
            this.Id = Id;
        }
    }

   
    // lookUp delete command handler with string response as output
    public class DeletePartnerCommmandHandler : IRequestHandler<DeletePartnerCommand, String>
    {
        private readonly IPartnerRepository _partnerRepository;
        public DeletePartnerCommmandHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository= partnerRepository;
        }

        public async Task<string> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var partnerEntity = await _partnerRepository.GetByIdAsync(request.Id);

                await _partnerRepository.DeleteAsync(partnerEntity);
            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Partner information has been deleted!";
        }
    }
}
 

