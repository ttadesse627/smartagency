
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
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

        var eagerLoadedProperties = new string[]
                                    {
                                        "PortOfArrival", "Priority","VisaType",
                                        "PortOfArrival","AttachmentFile","OrderCriteria",
                                        "OrderCriteria.Nationality","OrderCriteria.JobTitle",
                                        "OrderCriteria.Salary","OrderCriteria.Religion",
                                        "OrderCriteria.Experience","OrderCriteria.Language",
                                        "Sponsor","Sponsor.AttachmentFile","Sponsor.Address",
                                        "Sponsor.Address.AddressRegion","Sponsor.Address.Country",
                                        "Payment","Employee","Partner"
                                    };
        var serviceResponse = await _orderRepository.GetWithPredicateAsync(order => order.Id == editOrderRequest.Id, eagerLoadedProperties);

        var orderEntity = serviceResponse.First();
        if (orderEntity is not null)
        {
            if (!orderEntity.OrderCriteria!.Equals(null) || !orderEntity.Sponsor!.Equals(null) || !orderEntity.Payment!.Equals(null) || orderEntity.AttachmentFile!.Equals(null))
            {
                CustomMapper.Mapper.Map(request.editOrderRequest, orderEntity);
            }
            // updateResponse = _orderRepository.UpdateOrder(orderEntity);
            response = await _orderRepository.SaveDbUpdateAsync();
            if (response.Data >= 1)
            {
                response.Message = $"Successfully updated the order with an id {editOrderRequest.Id}";
                response.Success = true;
            }
        }
        else if (orderEntity is null)
        {
            response.Message = $"An order with an Id {editOrderRequest.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to update the order.");
        return response;
    }
}