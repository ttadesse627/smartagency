
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class GetForAssignmentQuery : IRequest<GetForAssignmentDTO>
{ }
public class GetForAssignmentQueryHandler : IRequestHandler<GetForAssignmentQuery, GetForAssignmentDTO>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public GetForAssignmentQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<GetForAssignmentDTO> Handle(GetForAssignmentQuery request, CancellationToken cancellationToken)
    {
        var orderResponse = new List<GetForAssignmentOrderDTO>();
        var response = new GetForAssignmentDTO();
        var ordEagerLoadedProps = new string[]
                                    {
                                        "OrderCriteria","OrderCriteria.JobTitle","Sponsor",
                                        "OrderCriteria.Language","OrderCriteria.Religion",
                                    };
        var orderList = await _orderRepository.GetAllWithPredicateAsync
                        (
                            order => order.IsDeleted == false && (order.Employees.Count == 0 || order.Employees != null), ordEagerLoadedProps
                        );

        if (orderList.Count > 0 || orderList != null)
        {
            foreach (var order in orderList)
            {
                var ordResp = new GetForAssignmentOrderDTO
                {
                    OrderId = order.Id,
                    OrderNumber = order.OrderNumber,
                    VisaNumber = order.VisaNumber,
                    FullName = order.Sponsor.FullName,
                    JobTitle = order.OrderCriteria.JobTitle.Value,
                    Religion = order.OrderCriteria.Religion.Value,
                    Language = order.OrderCriteria.JobTitle.Value,
                    Age = order.OrderCriteria.Age
                };
                orderResponse.Add(ordResp);
            }
        }
        response.UnAssignedOrders = orderResponse;
        return response;
    }
}