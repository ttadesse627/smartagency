using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetAllLookUpsHandler: IRequestHandler<GetAllLookUps, List<LookUpResponseDTO>>
{
    private readonly ILookUpRepository _lookUpRepository;

        public GetAllLookUpsHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<List<LookUpResponseDTO>> Handle(GetAllLookUps request, CancellationToken cancellationToken)
        {
            var lookUpList = await _lookUpRepository.GetAllAsync();
            var lookUpResponse = CustomMapper.Mapper.Map<List<LookUpResponseDTO>>(lookUpList);
            return lookUpResponse;
        }
}
}