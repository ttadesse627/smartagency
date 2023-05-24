using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public record CreateOrderCommand(CreateOrderRequest request) : IRequest<ServiceResponse<Int32>>
{ }
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var createOrderCommandResponse = new ServiceResponse<Int32>();
        
        var orderEntity = CustomMapper.Mapper.Map<Order>(command.request);
        orderEntity.Payment?.UpdatePayment(command.request.Payment.CurrentPaidAmount);

        await _orderRepository.InsertAsync(orderEntity, cancellationToken);
        createOrderCommandResponse.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
        if (createOrderCommandResponse.Success)
        {
            createOrderCommandResponse.Data = orderEntity.GetHashCode();
            createOrderCommandResponse.Message = $"Operation Succeeded: {createOrderCommandResponse.Data} entity is created!";
        }         

        return createOrderCommandResponse;
    }
}