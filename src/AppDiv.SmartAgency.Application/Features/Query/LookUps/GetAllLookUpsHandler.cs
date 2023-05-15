using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetAllLookUpsHandler : IRequestHandler<GetAllLookUps, SearchModel<LookUpResponseDTO>>
    {
        // private readonly ISmartAgencyDbContext _context;
        // private readonly IMapper _mapper;
        private readonly ILookUpRepository _lookUpRepository;

        public GetAllLookUpsHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<SearchModel<LookUpResponseDTO>> Handle(GetAllLookUps request, CancellationToken cancellationToken)
        {

            var lookUpList = await _lookUpRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, "Category");
            var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<LookUpResponseDTO>>(lookUpList);

            return paginatedListResp;
        }
    }
}