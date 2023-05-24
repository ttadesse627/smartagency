

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record AssignOrderCommand(OrderAssignmentRequest orderAssignmentRequest) : IRequest<ServiceResponse<Int32>>
{}

public class AssignOrderCommandHandler : IRequestHandler<AssignOrderCommand, ServiceResponse<Int32>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public AssignOrderCommandHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(AssignOrderCommand command, CancellationToken cancellationToken)
    {
        var assignOrderRequests = command.orderAssignmentRequest.OrderAssignments;
        var response = new ServiceResponse<int>();
        var updateResponse = new ServiceResponse<String>();
        var exceptions = new List<Exception>();
        var count = 0;

        if (assignOrderRequests != null)
        {
            foreach (var orderRequest in assignOrderRequests)
            {
                var orderEntity = await _orderRepository.GetWithPredicateAsync(order => order.Id == orderRequest.OrderId && order.EmployeeId == null, "Employee");
                if (orderEntity != null)
                {
                    var employee = await _applicantRepository.GetWithPredicateAsync(applicant =>
                        applicant.Id == orderRequest.EmployeeId
                        && applicant.IsDeleted == false, "Order");
                    if (employee != null)
                    {
                        if (employee.Order == null)
                        {
                            orderEntity.Employee = employee;
                            response = await _orderRepository.SaveDbUpdateAsync();
                        }
                    }
                    else
                    {
                        exceptions.Add(new Exception($"There is no Employee with id {orderRequest.EmployeeId}."));
                    }
                }
            }
        }
        return response;
    }
}