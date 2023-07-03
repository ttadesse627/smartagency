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
    public class GetNotAssignedVisaQuery : IRequest<List<NotAssignedVisaResponseDTO>>
    {
        
    }
    public class GetNotAssignedVisaHandler : IRequestHandler<GetNotAssignedVisaQuery, List<NotAssignedVisaResponseDTO>>
    {
         private readonly  IOrderRepository _orderRepository;

         public GetNotAssignedVisaHandler(IOrderRepository orderRepository)
         {
            _orderRepository= orderRepository;
         }
         
         public async Task<List<NotAssignedVisaResponseDTO>> Handle(GetNotAssignedVisaQuery request, CancellationToken cancellationToken)
         {
            return await _orderRepository.GetNotAssignedVisa();
         }
    }
}