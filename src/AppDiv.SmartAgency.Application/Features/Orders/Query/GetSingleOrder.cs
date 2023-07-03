
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public record GetSingleOrder(Guid id) : IRequest<ServiceResponse<GetOrderRespDTO>>
{ }

public class GetSingleOrderHandler : IRequestHandler<GetSingleOrder, ServiceResponse<GetOrderRespDTO>>
{
    private readonly IOrderRepository _orderRepository;

    public GetSingleOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<GetOrderRespDTO>> Handle(GetSingleOrder request, CancellationToken cancellationToken)
    {
        var orderResponse = new ServiceResponse<Order>();
        var orderResponseDTO = new ServiceResponse<GetOrderRespDTO>();
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
        orderResponse.Data = await _orderRepository.GetWithPredicateAsync(order => order.Id == request.id && order.IsDeleted == false, eagerLoadedProperties);

        if (orderResponse.Data is not null)
        {
            orderResponse.Success = true;
            orderResponseDTO = CustomMapper.Mapper.Map<ServiceResponse<GetOrderRespDTO>>(orderResponse);
        }
        else
        {
            orderResponseDTO.Message = orderResponse.Message;
        }
        return orderResponseDTO;
    }
}