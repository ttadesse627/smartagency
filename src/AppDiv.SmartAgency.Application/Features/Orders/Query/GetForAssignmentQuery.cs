

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class GetForAssignmentQuery : IRequest<List<GetForAssignmentOrderDTO>>
{}
public class GetForAssignmentQueryHandler : IRequestHandler<GetForAssignmentQuery, List<GetForAssignmentOrderDTO>>
{
    private readonly IOrderRepository _orderRepository;
    public GetForAssignmentQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<GetForAssignmentOrderDTO>> Handle(GetForAssignmentQuery request, CancellationToken cancellationToken)
    {
        var orderResponse = new List<GetForAssignmentOrderDTO>();
        var eagerLoadedProperties = new string[]
                                    {
                                        "OrderCriteria.Salary",  "OrderCriteria.JobTitle",
                                        "OrderCriteria.Language","OrderCriteria.Religion",
                                    };
        var orderList = await _orderRepository.GetWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.EmployeeId == null, eagerLoadedProperties
                        );
        orderResponse = CustomMapper.Mapper.Map(orderList, orderResponse);
        return orderResponse;
    }
}