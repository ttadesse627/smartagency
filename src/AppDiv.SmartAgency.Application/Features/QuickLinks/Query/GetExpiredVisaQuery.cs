
using System.Reflection.Metadata;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.QuickLinks.Query
{
    public class GetExpiredVisaQuery : IRequest<List<VisaExpiryResponseDTO>>
    {
        
    }

    public class GetExpiredVisaHandler : IRequestHandler<GetExpiredVisaQuery, List<VisaExpiryResponseDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetExpiredVisaHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            
        }

        public async Task<List<VisaExpiryResponseDTO>> Handle(GetExpiredVisaQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetExpiredVisa();
        }
    }
}