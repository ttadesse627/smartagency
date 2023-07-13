

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class GetForEnjazQuery : IRequest<List<DropdownEnjazResponseDTO>>
{ }
/*public class GetForEnjazQueryHandler : IRequestHandler<GetForEnjazQuery, List<DropdownEnjazResponseDTO>>

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
                                        "Employees","Employees.Jobtitle","Employees.Language","Enjaz"};

        var applEagerLoadedProps = new string[] { "Order", "Jobtitle", "Language", "Order.Enjaz" };

        var orderList = await _orderRepository.GetAllWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.Employees != null && order.Employees.Count > 0 && order.Enjaz == null, ordEagerLoadedProps
                        );

        var applicantList = await _applicantRepository.GetAllWithPredicateAsync
                        (
                            applicant => applicant.IsDeleted == false && applicant.Order == null, applEagerLoadedProps
                        );

        if (orderList.Count > 0 && orderList != null)
        {
            foreach (var order in orderList)
            {
                if (order.Employees != null && order.Employees.Count > 0)
                {
                    foreach (var empl in order.Employees)
                    {
                        var ordResp = new DropdownEnjazResponseDTO
                        {
                            OrderId = order.Id,
                            OrderNumber = order.OrderNumber,
                            SponsorFullName = order.Sponsor?.FullName,
                            EmployeeProfession = empl.Jobtitle.Value,
                            EmployeeLanguage = empl.Language.Value,
                            PassportNumber = empl.PassportNumber,
                            EmployeeFullName = empl.FirstName + " " + empl.MiddleName + " " + empl.LastName
                        };
                        response.Add(ordResp);
                    }
                }

            }
        }


        return response;
    }
}*/