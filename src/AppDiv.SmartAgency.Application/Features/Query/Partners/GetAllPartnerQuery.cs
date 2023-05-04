using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;

namespace AppDiv.SmartAgency.Application.Features.Query.Partners
{
    // Customer query with List<Customer> response
    public record GetAllPartnerQuery : IRequest<List<PartnerResponseDTO>>
    {

    }

    public class GetAllPartnerHandler : IRequestHandler<GetAllPartnerQuery, List<PartnerResponseDTO>>
    {
        private readonly IPartnerRepository _partnerRepository;

        public GetAllPartnerHandler(IPartnerRepository partnerQueryRepository)
        {
            _partnerRepository = partnerQueryRepository;
        }
        public async Task<List<PartnerResponseDTO>> Handle(GetAllPartnerQuery request, CancellationToken cancellationToken)
        {
            var partnerList = await _partnerRepository.GetAllWithAsync("Address");
            var partnerResponse = CustomMapper.Mapper.Map<List<PartnerResponseDTO>>(partnerList);
            return partnerResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}