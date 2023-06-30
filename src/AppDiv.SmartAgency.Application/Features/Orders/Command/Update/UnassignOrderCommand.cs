

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record UnassignOrderCommand(UnassignOrderRequest request) : IRequest<ServiceResponse<Int32>> { }

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
                var orderEntity = await _orderRepository.GetWithPredicateAsync(order => order.Id == orderId && (order.Employees != null && order.Employees.Count > 0) && order.IsDeleted == false, "Employees");
                if (orderEntity != null)
                {
                    foreach (var empl in orderEntity.Employees)
                    {
                        orderEntity.Employees.Remove(empl);
                    }
                }
                else exceptions.Add(new Exception($"There is no Order with id {orderId}."));
            }

            try
            {
                response.Success = await _orderRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception ex)
            {
                // TODO
            }
        }
        return response;
    }
}