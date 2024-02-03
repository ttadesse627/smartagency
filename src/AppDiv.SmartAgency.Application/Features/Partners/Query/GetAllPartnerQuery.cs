using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;

namespace AppDiv.SmartAgency.Application.Features.Partners.Query
{
    // Customer query with List<Customer> response
    public record GetAllPartnerQuery : IRequest<SearchModel<GetAllPartnerResponseDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;

        public GetAllPartnerQuery(int pageNumber, int pageSize, string? searchTerm, string? orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

    public class GetAllPartnerHandler : IRequestHandler<GetAllPartnerQuery, SearchModel<GetAllPartnerResponseDTO>>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly ISmartAgencyDbContext _dbContext;

        public GetAllPartnerHandler(IPartnerRepository partnerQueryRepository, ISmartAgencyDbContext dbContext)
        {
            _partnerRepository = partnerQueryRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<GetAllPartnerResponseDTO>> Handle(GetAllPartnerQuery request, CancellationToken cancellationToken)
        {
            var partnerList = await _partnerRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, null, "Address", "Address.Country");
            var partnerResponse = CustomMapper.Mapper.Map<SearchModel<GetAllPartnerResponseDTO>>(partnerList);
            return partnerResponse;

        }
    }
}