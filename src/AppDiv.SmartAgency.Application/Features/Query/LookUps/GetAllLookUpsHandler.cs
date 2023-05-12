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
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetAllLookUpsHandler : IRequestHandler<GetAllLookUps, PaginatedList<LookUpResponseDTO>>
    {
        // private readonly ISmartAgencyDbContext _context;
        // private readonly IMapper _mapper;
        private readonly ILookUpRepository _lookUpRepository;

        public GetAllLookUpsHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<PaginatedList<LookUpResponseDTO>> Handle(GetAllLookUps request, CancellationToken cancellationToken)
        {

            var lookUpList = await _lookUpRepository.GetAllWithSearchAsync(request.ColumnNames, request.SearchTerm, "Category");

            var paginatedListLookUp = new PaginatedList<LookUp>((IReadOnlyCollection<LookUp>)lookUpList, lookUpList.Count(), request.PageNumber, request.PageSize);
            var paginatedList = await PaginatedList<LookUp>.CreateAsync(lookUpList, request.PageNumber, request.PageSize);
            var paginatedListResp = CustomMapper.Mapper.Map<PaginatedList<LookUpResponseDTO>>(paginatedList);

            return paginatedListResp;
        }
    }
}