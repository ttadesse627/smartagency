using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public class GetUnAssignedOrdersDrodownQuery : IRequest<List<GetUnAssignedOrdersDropdownResponseDTO>>
    {
    }

    public class GetUnAssignedOrdersDrodownHandler : IRequestHandler<GetUnAssignedOrdersDrodownQuery , List<GetUnAssignedOrdersDropdownResponseDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetUnAssignedOrdersDrodownHandler(IOrderRepository orderRepository)
        {
            _orderRepository= orderRepository;
        }
        public async Task<List<GetUnAssignedOrdersDropdownResponseDTO>> Handle(GetUnAssignedOrdersDrodownQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetUnAssignedOrdersDropDown();
        }
    }
}