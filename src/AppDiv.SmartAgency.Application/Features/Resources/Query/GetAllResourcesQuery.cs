using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Resources.Query
{
    public record GetAllResourcesQuery : IRequest<IEnumerable<ResourceResponseDTO>>{}

     public class GetAllResourcesHandler : IRequestHandler<GetAllResourcesQuery, IEnumerable<ResourceResponseDTO>>
    {
        private readonly ISmartAgencyDbContext _dbContext;
        private readonly IResourceRepository _ResourceRepository;

        public GetAllResourcesHandler(IResourceRepository ResourceRepository, ISmartAgencyDbContext dbContext)
        {
            _ResourceRepository = ResourceRepository;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ResourceResponseDTO>> Handle(GetAllResourcesQuery request, CancellationToken cancellationToken)
        {
            var resources = await _ResourceRepository.GetAllAsync(res => res.Name,0, 100);
            var resourcesResponse = resources.Select(resource => new ResourceResponseDTO{Id = resource.Id, Name = resource.Name});
            return resourcesResponse;
        }
    }
}