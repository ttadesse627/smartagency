using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
public record CreateOrderCommand(CreateOrderRequest request) : IRequest<ServiceResponse<String>>
{ }
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<String>>
{
    private readonly IOrderRepository _orderRepository;
    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<String>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderEntity = CustomMapper.Mapper.Map<Order>(command.request);

        var createOrderCommandResponse = new ServiceResponse<string>();

        // var validator = new CreateAttachmentCommandValidator(_attachmentRepository);
        // var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //can use this instead of automapper
        await _orderRepository.InsertAsync(orderEntity, cancellationToken);
        createOrderCommandResponse.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
        if (createOrderCommandResponse.Success)
        {
            createOrderCommandResponse.Data = "You have successfully created order entity.";
            createOrderCommandResponse.Message = "Operation Succeeded";
        }

        //var customerResponse = CustomerMapper.Mapper.Map<CustomerResponseDTO>(customer);
        // createCustomerCommandResponse.Customer = customerResponse;          

        return createOrderCommandResponse;
    }
}