
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ResourceDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Resources.Query
{
    public record GetAllResourcesQuery : IRequest<SearchModel<ResourceResponseDTO>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllResourcesQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

     public class GetAllResourcesHandler : IRequestHandler<GetAllResourcesQuery, SearchModel<ResourceResponseDTO>>
    {
        private readonly ISmartAgencyDbContext _dbContext;
        private readonly IResourceRepository _ResourceRepository;

        public GetAllResourcesHandler(IResourceRepository ResourceRepository, ISmartAgencyDbContext dbContext)
        {
            _ResourceRepository = ResourceRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<ResourceResponseDTO>> Handle(GetAllResourcesQuery request, CancellationToken cancellationToken)
        {
            var paginatedResource = await _ResourceRepository.GetAllWithAsync();
            var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<ResourceResponseDTO>>(paginatedResource);

            return paginatedListResp;
        }
    }
}