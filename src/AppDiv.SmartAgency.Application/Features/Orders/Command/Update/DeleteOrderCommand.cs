
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Utility.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record DeleteOrderCommand(Guid Id) : IRequest<ServiceResponse<int>> { }

public class DeleteOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<ServiceResponse<int>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<int>();

        var deletedOrder = await _orderRepository.GetAsync(request.Id);
        if (deletedOrder is not null) deletedOrder.IsDeleted = true;
        try
        {
            response.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                response.Message = $"Successfully deleted the order with an id {request.Id}";
                response.Data += 1;
            }
            else
            {
                throw new NotFoundException("You are trying to access the order that does not exist!");
            }
        }
        catch (Exception ex)
        {
            response.Message = "An error occurred while trying to delete order.";
            response.Errors?.Add(ex.Message);
            throw new ApplicationException("An error occurred while trying to delete order!");
        }

        return response;
    }
}