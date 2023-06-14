using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Deposits.Command.Delete
{
    public record DeleteDepositCommand(List<Guid> Ids): IRequest<String>
    {

    }

   
    // lookUp delete command handler with string response as output
    public class DeleteDepositCommmandHandler : IRequestHandler<DeleteDepositCommand, String>
    {
        private readonly IDepositRepository _depositRepository;
        
        public DeleteDepositCommmandHandler(IDepositRepository depositRepository)
        {
            _depositRepository= depositRepository;
          
        }

        public async Task<string> Handle(DeleteDepositCommand request, CancellationToken cancellationToken)
        {
            int response= 0;
            try
            {
                 response = await _depositRepository.DeleteMany(request.Ids);
                
            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return response+" "+"deleted!";
        }
    }
}