
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Update.Orders;
public class RestoreDeleteOrderCommand : IRequest<ServiceResponse<Int32>>
{
    public Guid Id { get; set; }
    public RestoreDeleteOrderCommand(Guid id)
    {
        Id = id;
    }
}

public class RestoreDeleteOrderCommandHandler : IRequestHandler<RestoreDeleteOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    public RestoreDeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(RestoreDeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<int>();

        var serviceResponse = await _orderRepository.GetOrderAsync(request.Id);
        var orderToBeRestored = serviceResponse.Data;
        if (orderToBeRestored is not null)
        {
            orderToBeRestored.IsDeleted = false;
            response = await _orderRepository.SaveDbUpdateAsync();
            if (response.Data >= 1)
            {
                response.Message = $"Successfully Restored the applicant with an id {request.Id}";
                response.Success = true;
            }
        }
        else if (orderToBeRestored is null)
        {
            response.Message = $"An applicant with an Id {request.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to delete the order.");
        return response;
    }
}