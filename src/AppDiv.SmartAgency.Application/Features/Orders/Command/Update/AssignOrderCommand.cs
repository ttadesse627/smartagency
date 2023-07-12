

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record AssignOrderCommand(OrderAssignmentRequest orderAssignmentRequest) : IRequest<ServiceResponse<Int32>>
{ }

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

        if (assignOrderRequests != null)
        {
            foreach (var orderRequest in assignOrderRequests)
            {
                var orderEntity = await _orderRepository.GetWithPredicateAsync(order => order.Id == orderRequest.OrderId, "Employees");
                var employee = await _applicantRepository.GetWithPredicateAsync(applicant => applicant.Id == orderRequest.EmployeeId && applicant.IsDeleted == false && applicant.OrderId==null, "Order");

                if (orderEntity != null && employee != null)
                {
                    if (employee.OrderId == null)
                    {
                        employee.OrderId = orderEntity.Id;
                    }
                }

                try
                {
                    response.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
                    if (response.Success)
                    {
                        response.Data += 1;
                    }
                }
                catch (System.Exception ex)
                {
                    response.Message = "An error occured while saving the assignment.";
                    response.Errors?.Add(ex.Message);
                }

            }
        }
        return response;
    }
}