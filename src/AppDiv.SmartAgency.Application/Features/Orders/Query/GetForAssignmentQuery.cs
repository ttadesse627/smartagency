
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class GetForAssignmentQuery : IRequest<ResponseContainerDTO<List<GetForAssignmentOrderDTO>>>
{ }
public class GetForAssignmentQueryHandler : IRequestHandler<GetForAssignmentQuery, ResponseContainerDTO<List<GetForAssignmentOrderDTO>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public GetForAssignmentQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<ResponseContainerDTO<List<GetForAssignmentOrderDTO>>> Handle(GetForAssignmentQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainerDTO<List<GetForAssignmentOrderDTO>>();
        var orderResponse = new List<GetForAssignmentOrderDTO>();
        var ordEagerLoadedProps = new string[]
                                    {
                                        "OrderCriteria","OrderCriteria.JobTitle","Sponsor",
                                        "OrderCriteria.Language","OrderCriteria.Religion", "Employees",
                                    };
        var orderList = await _orderRepository.GetAllWithPredicateAsync
                        (
                            order => (order.Employees == null || order.Employees.Count() < order.NumberOfVisa) && (order.IsDeleted == false), ordEagerLoadedProps
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
                    FullName = order.Sponsor?.FullName,
                    JobTitle = order.OrderCriteria?.JobTitle?.Value,
                    Religion = order.OrderCriteria?.Religion?.Value,
                    Language = order.OrderCriteria?.Language?.Value,
                    Age = order.OrderCriteria?.Age,
                    NumberOfAvailableVisa = order.NumberOfVisa - (order.Employees?.Count) ?? 0
                };
                orderResponse.Add(ordResp);
            }
        }
        response.Items = orderResponse;
        return response;
    }
}