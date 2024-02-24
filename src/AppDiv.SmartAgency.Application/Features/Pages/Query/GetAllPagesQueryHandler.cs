using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Query
{
    public class GetAllPagesQueryHandler : IRequestHandler<GetAllPagesQuery, SearchModel<PageResponseDTO>>
    {
        private readonly IPageRepository _pageRepository;

        public GetAllPagesQueryHandler(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public async Task<SearchModel<PageResponseDTO>> Handle(GetAllPagesQuery request, CancellationToken cancellationToken)
        {
            var pageList = await _pageRepository.GetAllWithSearchAsync(request.SearchTerm!);
            var paginatedPages = await _pageRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, pageList, request.OrderBy);
            var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<PageResponseDTO>>(paginatedPages);

            return paginatedListResp;
        }
    }
}





