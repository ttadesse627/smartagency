


using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Query;
public record GetOrderComplaintsQuery(Guid OrderId) : IRequest<GetOrderComplaintsResponseDTO> { }
public class GetOrderComplaintsQueryHandler : IRequestHandler<GetOrderComplaintsQuery, GetOrderComplaintsResponseDTO>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderComplaintsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderComplaintsResponseDTO> Handle(GetOrderComplaintsQuery request, CancellationToken cancellationToken)
    {
        var response = new GetOrderComplaintsResponseDTO();
        var explLoadedProps = new string[] { "Employee", "Sponsor", "Partner", "Complaints", "Employee.Address", "Sponsor.Address" };
        var order = await _orderRepository.GetWithPredicateAsync(order => order.Id == request.OrderId, explLoadedProps);

        if (order != null)
        {
            if (order.Employee != null)
            {
                var employeeName = order.Employee.FirstName + " " + order.Employee.MiddleName + " " + order.Employee.LastName;
                var employeeInfo = new EmployeeInfoDTO
                {
                    Id = order.Employee.Id,
                    EmployeeName = employeeName,
                    HouseNumber = order.Employee.Address?.HouseNumber,
                    PhoneNumber = order.Employee.Address?.PhoneNumber,
                    MobileNumber = order.Employee.Address?.Mobile
                };
            }
            var sponsorInfo = new SponsorInfoDTO
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerName = order.Partner?.PartnerName,
                VisaNumber = order.VisaNumber,
                SponsorName = order.Sponsor?.FullName,
                HousePhone = order.Sponsor?.Address?.PhoneNumber,
                MobilePhone = order.Sponsor?.Address?.Mobile
            };

            if (order.Complaints != null || order.Complaints?.Count > 0)
            {
                var compRespList = new List<GetComplaintResponseDTO>();

                foreach (var complaint in order.Complaints)
                {
                    var comResponse = new GetComplaintResponseDTO
                    {
                        Message = complaint.Message,
                        SenderName = complaint.User.FullName,
                        Date = complaint.CreatedAt
                    };
                    compRespList.Add(comResponse);
                }
            }
        }

        return response;

    }
}