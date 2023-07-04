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
    public class GetComplaintQuery :IRequest<List<ComplaintResponseDTO>>
    {
        
    }
    public class GetComplaintHandler : IRequestHandler<GetComplaintQuery, List<ComplaintResponseDTO>>
    {
       private readonly IOrderRepository _orderRepository;

       public GetComplaintHandler(IOrderRepository orderRepository)
       {
          _orderRepository= orderRepository;
       }

       public async Task<List<ComplaintResponseDTO>> Handle(GetComplaintQuery request, CancellationToken cancellationToken)
       {
           return await _orderRepository.GetComplaints();


       }

    }
}