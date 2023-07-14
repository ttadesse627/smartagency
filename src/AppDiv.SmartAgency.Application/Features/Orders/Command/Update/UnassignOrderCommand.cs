

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
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
                var applicantEntity = await _applicantRepository.GetWithPredicateAsync(applicant => applicant.OrderId == orderId && applicant.IsDeleted == false, "Order");
                if (applicantEntity != null)
                {
                    applicantEntity.Order = null;
                }
                else exceptions.Add(new NotFoundException($"The Order with id {orderId} has no applicant assigned."));
            }

            try
            {
                response.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Message = "Un assigned successfully!";
                }
            }
            catch (System.Exception ex)
            {
                response.Errors?.Add(ex.Message);
            }
        }
        return response;
    }
}