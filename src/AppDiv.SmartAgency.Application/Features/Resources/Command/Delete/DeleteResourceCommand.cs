using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Resources.Command.Delete
{
    public class DeleteResourceCommand(Guid id) : IRequest<string>
    {
        public Guid Id { get; private set; } = id;
    }


    // Resource delete command handler with string response as output
    public class DeleteResourceCommmandHandler : IRequestHandler<DeleteResourceCommand, String>
    {
        private readonly IResourceRepository _resourceRepository;
        public DeleteResourceCommmandHandler(IResourceRepository ResourceRepository)
        {
            _resourceRepository = ResourceRepository;
        }

        public async Task<string> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ResourceEntity = await _resourceRepository.GetByIdAsync(request.Id);
                if (ResourceEntity is not null)
                {
                    await _resourceRepository.DeleteAsync(ResourceEntity.Id);
                    await _resourceRepository.SaveChangesAsync(cancellationToken);
                }
                else return $"No entity found with an Id {request.Id}";

            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            return "Resource information has been deleted!";
        }
    }
}


