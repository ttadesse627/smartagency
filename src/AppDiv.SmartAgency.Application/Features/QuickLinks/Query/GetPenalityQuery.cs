using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.QuickLinks.Query
{
    public class GetPenalityQuery : IRequest<List<PenalityResponseDTO>>
    {
        
    }
    public class GetPenalityHandler : IRequestHandler<GetPenalityQuery , List<PenalityResponseDTO>>
    {
        private readonly IOrderRepository _orderRepository; 

        public GetPenalityHandler(IOrderRepository orderRepository)
        {

            _orderRepository = orderRepository;
        }
        public async Task<List<PenalityResponseDTO>>  Handle(GetPenalityQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetPenality();
        }
    }
}