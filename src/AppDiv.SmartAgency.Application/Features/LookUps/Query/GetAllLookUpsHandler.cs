using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Query
{
    public class GetAllLookUpsHandler : IRequestHandler<GetAllLookUps, SearchModel<LookUpResponseDTO>>
    {
        private readonly ILookUpRepository _lookUpRepository;

        public GetAllLookUpsHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<SearchModel<LookUpResponseDTO>> Handle(GetAllLookUps request, CancellationToken cancellationToken)
        {
            var attachmentList = await _lookUpRepository.GetAllWithSearchAsync(request.SearchTerm);
            var paginatedLookup = await _lookUpRepository.PaginateItems(request.PageNumber, request.PageSize, request.SortingDirection, attachmentList, request.OrderBy);
            var paginatedListResp = CustomMapper.Mapper.Map<SearchModel<LookUpResponseDTO>>(paginatedLookup);

            return paginatedListResp;
        }
    }
}