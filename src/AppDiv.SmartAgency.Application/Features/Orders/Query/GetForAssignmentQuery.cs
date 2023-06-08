

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
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
        var applicantResponse = new List<GetApplForAssignmentDTO>();
        var orderResponse = new List<GetForAssignmentOrderDTO>();
        var applEagerLoadedProps = new string[] { "Order", "Jobtitle", "Language", "Religion", "Salary" };
        var ordEagerLoadedProps = new string[]
                                    {
                                        "OrderCriteria.Salary",  "OrderCriteria.JobTitle",
                                        "OrderCriteria.Language","OrderCriteria.Religion",
                                    };

        var applicantList = await _applicantRepository.GetAllWithPredicateAsync
                        (
                            applicant => applicant.IsDeleted == false && applicant.Order == null, applEagerLoadedProps
                        );
        var orderList = await _orderRepository.GetAllWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.EmployeeId == null, ordEagerLoadedProps
                        );

        applicantResponse = CustomMapper.Mapper.Map(applicantList, applicantResponse);
        orderResponse = CustomMapper.Mapper.Map(orderList, orderResponse);
        var response = new GetForAssignmentDTO
        {
            UnAssignedApplicants = applicantResponse,
            UnAssignedOrders = orderResponse
        };


        return response;
    }
}