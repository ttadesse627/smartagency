
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.DeletedInfos.Query;
public record GetDeletedOrderQuery(Guid id) : IRequest<GetOrderRespDTO>
{ }

public class GetDeletedOrderQueryHandler : IRequestHandler<GetDeletedOrderQuery, GetOrderRespDTO>
{
    private readonly IOrderRepository _orderRepository;

    public GetDeletedOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<GetOrderRespDTO> Handle(GetDeletedOrderQuery request, CancellationToken cancellationToken)
    {
        var orderResponse = new ServiceResponse<Order>();
        var orderResponseDTO = new GetOrderRespDTO();
        var eagerLoadedProperties = new string[]
                                    {
                                        "PortOfArrival", "Priority","VisaType",
                                        "PortOfArrival","AttachmentFile","OrderCriteria",
                                        "OrderCriteria.Nationality","OrderCriteria.JobTitle",
                                        "OrderCriteria.Salary","OrderCriteria.Religion",
                                        "OrderCriteria.Experience","OrderCriteria.Language",
                                        "Sponsor","Sponsor.AttachmentFile","Sponsor.Address",
                                        "Sponsor.Address.Region","Sponsor.Address.Country",
                                        "Sponsor.Address.City","Payment","Employees","Partner"
                                    };
        orderResponse.Data = await _orderRepository.GetWithPredicateAsync(order => order.Id == request.id && order.IsDeleted == true, eagerLoadedProperties);

        if (orderResponse.Data != null)
        {
            orderResponse.Success = true;
            orderResponseDTO = CustomMapper.Mapper.Map<GetOrderRespDTO>(orderResponse);
        }
        return orderResponseDTO;
    }
}