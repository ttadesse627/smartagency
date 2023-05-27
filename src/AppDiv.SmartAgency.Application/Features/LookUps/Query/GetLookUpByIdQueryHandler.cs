using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Query
{
    public class GetLookUpByIdQueryHandler : IRequestHandler<GetLookUpByIdQuery, LookUpResponseDTO>
    {
        private readonly ILookUpRepository _lookUpRepository;

        public GetLookUpByIdQueryHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
           
        }
        public async Task<LookUpResponseDTO> Handle(GetLookUpByIdQuery request, CancellationToken cancellationToken)
        {
            var lookUp = await _lookUpRepository.GetWithPredicateAsync(l=>l.Id== request.Id, "Category");
            return CustomMapper.Mapper.Map<LookUpResponseDTO>(lookUp);
        }
    }
}