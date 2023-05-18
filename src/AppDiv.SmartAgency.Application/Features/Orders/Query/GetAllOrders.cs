using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Orders;

public class GetAllOrders : IRequest<List<OrderResponseDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
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
        var paginatedList = new PaginatedList<Order>((IReadOnlyCollection<Order>)orderList, orderList.Count(), request.PageNumber, request.PageSize);
        var orderResponse = CustomMapper.Mapper.Map<List<OrderResponseDTO>>(orderList);
        return orderResponse;
    }
}