
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.DeletedInfos;

public class GetDeletedOrders : IRequest<List<OrderResponseDTO>>
{
}

public class GetDeletedOrdersHandler : IRequestHandler<GetDeletedOrders, List<OrderResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;

    public GetDeletedOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<List<OrderResponseDTO>> Handle(GetDeletedOrders request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetAllWithAsync
                        (order => order.IsDeleted == true,
                            "Partner", "PortOfArrival", "Priority",
                            "VisaType", "Employee", "VisaFile",
                            "OrderCriteria", "OrderPayment", "OrderSponsor", "VisaFile.FileCollectionAttachment");
        var orderResponse = CustomMapper.Mapper.Map<List<OrderResponseDTO>>(orderList);
        return orderResponse;
    }
}