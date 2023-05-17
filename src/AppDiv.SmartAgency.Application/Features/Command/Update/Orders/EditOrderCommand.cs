
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Update.Orders;
public record EditOrderCommand(EditOrderRequest editOrderRequest) : IRequest<ServiceResponse<Int32>>
{
}

public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    public EditOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        var editOrderRequest = request.editOrderRequest;
        var response = new ServiceResponse<int>();
        var updateResponse = new ServiceResponse<String>();
        var serviceResponse = await _orderRepository.GetOrderAsync(editOrderRequest.Id);

        var orderToBeEdited = serviceResponse.Data;
        if (orderToBeEdited is not null)
        {
            if (!orderToBeEdited.OrderCriteria.Equals(null) || !orderToBeEdited.OrderSponsor.Equals(null) || !orderToBeEdited.OrderPayment.Equals(null) || orderToBeEdited.OrderVisaFile!.Equals(null))
            {
                orderToBeEdited = CustomMapper.Mapper.Map<Order>(request.editOrderRequest);
            }
            updateResponse = _orderRepository.UpdateOrder(orderToBeEdited);
            response = await _orderRepository.SaveDbUpdateAsync();
            if (response.Data >= 1)
            {
                response.Message = $"Successfully updated the order with an id {editOrderRequest.Id}";
                response.Success = true;
            }
        }
        else if (orderToBeEdited is null)
        {
            response.Message = $"An order with an Id {editOrderRequest.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to update the order.");
        return response;
    }
}