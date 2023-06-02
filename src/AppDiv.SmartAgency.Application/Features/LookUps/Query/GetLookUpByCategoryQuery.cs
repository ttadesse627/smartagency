using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Query
{
    public record GetLookUpByCategoryQuery(List<string> categories) : IRequest<List<LookUpResponseDTO>> { }
    public class GetLookUpByCategoryQueryHandler : IRequestHandler<GetLookUpByCategoryQuery, List<LookUpResponseDTO>>
    {
        private readonly ILookUpRepository _lookUpRepository;

        public GetLookUpByCategoryQueryHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<List<LookUpResponseDTO>> Handle(GetLookUpByCategoryQuery request, CancellationToken cancellationToken)
        {
            var lookUps = await _lookUpRepository.GetAllKeysAsync(request.categories);
            return CustomMapper.Mapper.Map<List<LookUpResponseDTO>>(lookUps);
        }
    }
}