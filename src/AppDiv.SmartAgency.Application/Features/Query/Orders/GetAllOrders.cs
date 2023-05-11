using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Orders;

public class GetAllOrders : IRequest<List<OrderResponseDTO>>
{
}

public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, List<OrderResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<List<OrderResponseDTO>> Handle(GetAllOrders request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetAllWithAsync
                        (
                            "Partner", "PortOfArrival", "Priority",
                            "VisaType", "Employee", "VisaFile",
                            "OrderCriteria", "OrderPayment", "OrderSponsor", "VisaFile.FileCollectionAttachment");
        var orderResponse = CustomMapper.Mapper.Map<List<OrderResponseDTO>>(orderList);
        return orderResponse;
    }
}