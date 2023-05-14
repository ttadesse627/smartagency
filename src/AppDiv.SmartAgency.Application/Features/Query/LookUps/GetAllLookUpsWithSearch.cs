using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public record GetAllLookUpsWithSearch : IRequest<SearchModel<LookUpResponseDTO>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string[] ColumnNames { get; set; } = new string[] { "Value" };
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public object OrderBy { get; set; }
        public GetAllLookUpsWithSearch(string searchTerm = "", int pageNumber = 1, int pageSize = 10, object? orderBy = null)
        {
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;
        }

    }

    public class GetAllLookUpsWithSearchHandler : IRequestHandler<GetAllLookUpsWithSearch, SearchModel<LookUpResponseDTO>>
    {
        // private readonly ISmartAgencyDbContext _context;
        // private readonly IMapper _mapper;
        private readonly ILookUpRepository _lookUpRepository;

        public GetAllLookUpsWithSearchHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<SearchModel<LookUpResponseDTO>> Handle(GetAllLookUpsWithSearch request, CancellationToken cancellationToken)
        {

            // var lookUpList = await _lookUpRepository.GetAllWithSearchAsync(request.ColumnNames, request.SearchTerm, "Category");

            // var paginatedListLookUp = new SearchModel<LookUp>();
            // var lookup = new LookUp();
            var paginatedList = await _lookUpRepository.GetPagedSearchResultWithAsync(lk => lk.Value.Contains(request.SearchTerm), lkup => lkup.Value, request.PageNumber, request.PageSize, SortingDirection.Ascending, "Category");
            var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<LookUpResponseDTO>>(paginatedList);

            return paginatedListResp;
        }
    }
}