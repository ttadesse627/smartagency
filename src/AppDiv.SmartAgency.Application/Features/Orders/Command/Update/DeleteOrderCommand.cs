
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public class DeleteOrderCommand : IRequest<ServiceResponse<Int32>>
{
    public Guid Id { get; set; }
    public DeleteOrderCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<int>();

        var serviceResponse = await _orderRepository.GetOrderAsync(request.Id);
        var orderToBeDeleted = serviceResponse.Data;
        if (orderToBeDeleted is not null)
        {
            orderToBeDeleted.IsDeleted = true;
            response = await _orderRepository.SaveDbUpdateAsync();
            if (response.Data >= 1)
            {
                response.Message = $"Successfully deleted the Order with an id {request.Id}";
                response.Success = true;
            }
        }
        else if (orderToBeDeleted is null)
        {
            response.Message = $"An Order with an Id {request.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to delete the order.");
        return response;
    }
}