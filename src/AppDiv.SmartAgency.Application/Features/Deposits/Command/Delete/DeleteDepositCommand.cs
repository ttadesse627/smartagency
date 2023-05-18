using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Delete.Deposits
{
    public class DeleteDepositCommand: IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeleteDepositCommand(Guid Id)
        {
            this.Id = Id;
        }
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
            try
            {
                var depositEntity = await _depositRepository.GetByIdAsync(request.Id);
                await _depositRepository.DeleteAsync(depositEntity.Id);
                 await _depositRepository.SaveChangesAsync(cancellationToken);
                

            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Deposit information has been deleted!";
        }
    }
}