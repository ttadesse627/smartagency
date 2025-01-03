using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Delete
{
    public class DeleteLookUpCommand : IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeleteLookUpCommand(Guid Id)
        {
            this.Id = Id;
        }
    }


    // lookUp delete command handler with string response as output
    public class DeleteLookUpCommmandHandler : IRequestHandler<DeleteLookUpCommand, String>
    {
        private readonly ILookUpRepository _lookUpRepository;
        public DeleteLookUpCommmandHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }

        public async Task<string> Handle(DeleteLookUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lookUpEntity = await _lookUpRepository.GetByIdAsync(request.Id);
                if (lookUpEntity is not null)
                {
                    await _lookUpRepository.DeleteAsync(lookUpEntity.Id);
                    await _lookUpRepository.SaveChangesAsync(cancellationToken);
                }
                else return $"No entity found with an Id {request.Id}";

            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            return "LookUp information has been deleted!";
        }
    }
}


