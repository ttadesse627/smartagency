using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Resources.Query
{
    public record GetResourceByIdQuery: IRequest<ResourceResponseDTO>
    {
        public Guid Id { get; private set; }

        public GetResourceByIdQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
    public class GetResourceByIdQueryHandler : IRequestHandler<GetResourceByIdQuery, ResourceResponseDTO>
    {
        private readonly IResourceRepository _resourceRepository;

        public GetResourceByIdQueryHandler(IResourceRepository ResourceRepository)
        {
            _resourceRepository = ResourceRepository;

        }
        public async Task<ResourceResponseDTO> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var Resource = await _resourceRepository.GetWithPredicateAsync(l => l.Id == request.Id);
            return CustomMapper.Mapper.Map<ResourceResponseDTO>(Resource);
        }
    }
}