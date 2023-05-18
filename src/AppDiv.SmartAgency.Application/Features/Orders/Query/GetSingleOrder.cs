
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class GetSingleOrder : IRequest<OrderResponseDTO>
{
    public Guid Id { get; set; }
    public GetSingleOrder(Guid id)
    {
        Id = id;
    }
}

public class GetSingleOrderHandler : IRequestHandler<GetSingleOrder, OrderResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public GetSingleOrderHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }
    public async Task<OrderResponseDTO> Handle(GetSingleOrder request, CancellationToken cancellationToken)
    {
        // var orders = await _mediator.Send(new GetAllOrders());
        var serviceResponse = new ServiceResponse<Order>();
        var returnedOrder = new OrderResponseDTO();
        serviceResponse = await _orderRepository.GetOrderAsync(request.Id);
        if (serviceResponse.Data is not null)
        {
            var selectedOrder = serviceResponse.Data;
            returnedOrder = CustomMapper.Mapper.Map<OrderResponseDTO>(selectedOrder);
        }
        return returnedOrder;
    }
}