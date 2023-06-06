using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Query
{
    public class GetAllLookUpsHandler : IRequestHandler<GetAllLookUps, SearchModel<LookUpResponseDTO>>
    {
        private readonly ISmartAgencyDbContext _dbContext;
        private readonly ILookUpRepository _lookUpRepository;

        public GetAllLookUpsHandler(ILookUpRepository lookUpRepository, ISmartAgencyDbContext dbContext)
        {
            _lookUpRepository = lookUpRepository;
            _dbContext = dbContext;
        }
        public async Task<SearchModel<LookUpResponseDTO>> Handle(GetAllLookUps request, CancellationToken cancellationToken)
        {

            var lookUpList = await _lookUpRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection, lk => lk.CreatedBy == "00000000-0000-0000-0000-000000000000");
            var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<LookUpResponseDTO>>(lookUpList);

            return paginatedListResp;
        }
    }
}