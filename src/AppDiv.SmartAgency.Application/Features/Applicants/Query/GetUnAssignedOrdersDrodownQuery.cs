
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{
    public class GetUnAssignedOrdersDrodownQuery : IRequest<GetUnAssignedOrdersResponseDTO>
    {
    }

    public class GetUnAssignedOrdersDrodownHandler : IRequestHandler<GetUnAssignedOrdersDrodownQuery, GetUnAssignedOrdersResponseDTO>
    {
        private readonly IOrderRepository _orderRepository;
        public GetUnAssignedOrdersDrodownHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<GetUnAssignedOrdersResponseDTO> Handle(GetUnAssignedOrdersDrodownQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetUnAssignedOrdersDropDown();
        }
    }
}