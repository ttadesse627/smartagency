

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record UnassignOrderCommand(UnassignOrderRequest request) : IRequest<ServiceResponse<Int32>>{}

public class UnassignOrderCommandHandler : IRequestHandler<UnassignOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public UnassignOrderCommandHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(UnassignOrderCommand command, CancellationToken cancellationToken)
    {
        var orderIds = command.request.OrderIds;
        var response = new ServiceResponse<int>();
        var updateResponse = new ServiceResponse<String>();
        var exceptions = new List<Exception>();

        if (orderIds != null)
        {
            foreach (var orderId in orderIds)
            {
                var orderEntity = await _orderRepository.GetWithPredicateAsync(order => order.Id == orderId && order.IsDeleted == false, "Employee");
                if (orderEntity != null)
                {
                    if (orderEntity.Employee != null)
                    {
                        orderEntity.Employee = null;
                        response = await _orderRepository.SaveDbUpdateAsync();
                    }
                    else
                    {
                        exceptions.Add(new Exception($"The order with id {orderId} is already unassigned."));
                    }
                }
                else exceptions.Add(new Exception($"There is no Order with id {orderId}."));
            }
        }
        return response;
    }
}