

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class GetForEnjazQuery : IRequest<List<DropdownEnjazResponseDTO>>
{ }
public class GetForEnjazQueryHandler : IRequestHandler<GetForEnjazQuery, List<DropdownEnjazResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public GetForEnjazQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<List<DropdownEnjazResponseDTO>> Handle(GetForEnjazQuery request, CancellationToken cancellationToken)
    {
        var response = new List<DropdownEnjazResponseDTO>();
        var ordEagerLoadedProps = new string[]{"Sponsor","OrderCriteria.Language","OrderCriteria.Religion",
                                        "Employee","Employee.Jobtitle","Employee.Language","Enjaz"};

        // var applicantList = await _applicantRepository.GetAllWithPredicateAsync
        //                 (
        //                     applicant => applicant.IsDeleted == false && applicant.Order == null, applEagerLoadedProps
        //                 );
        var orderList = await _orderRepository.GetAllWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.EmployeeId != null && order.Enjaz == null, ordEagerLoadedProps
                        );

        if (orderList.Count > 0 || orderList != null)
        {
            foreach (var order in orderList)
            {
                var ordResp = new DropdownEnjazResponseDTO
                {
                    OrderId = order.Id,
                    OrderNumber = order.OrderNumber,
                    SponsorFullName = order.Sponsor.FullName,
                    EmployeeProfession = order.Employee.Jobtitle.Value,
                    EmployeeLanguage = order.Employee.Language.Value,
                    PassportNumber = order.Employee.PassportNumber,
                    EmployeeFullName = order.Employee.FirstName + " " + order.Employee.MiddleName + " " + order.Employee.LastName
                };
                response.Add(ordResp);
            }
        }


        return response;
    }
}