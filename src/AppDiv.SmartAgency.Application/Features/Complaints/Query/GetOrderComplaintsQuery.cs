


using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Query;
public record GetOrderComplaintsQuery(Guid OrderId) : IRequest<GetOrderComplaintsResponseDTO> { }
public class GetOrderComplaintsQueryHandler : IRequestHandler<GetOrderComplaintsQuery, GetOrderComplaintsResponseDTO>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IComplaintRepository _complaintRepository;
    public GetOrderComplaintsQueryHandler(IOrderRepository orderRepository, IComplaintRepository complaintRepository)
    {
        _orderRepository = orderRepository;
        _complaintRepository = complaintRepository;
    }

    public async Task<GetOrderComplaintsResponseDTO> Handle(GetOrderComplaintsQuery request, CancellationToken cancellationToken)
    {
        var response = new GetOrderComplaintsResponseDTO();
        var explLoadedProps = new string[] { "Employee", "Sponsor", "Partner", "Complaints", "Complaints.User", "Employee.Address", "Sponsor.Address" };
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
                response.EmployeeInfo = employeeInfo;
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

            response.SponsorInfo = sponsorInfo;

            var complaints = await _complaintRepository.GetAllWithPredicateAsync(comp => comp.OrderId == request.OrderId, "User");
            var compResponse = new List<GetComplaintResponseDTO>();
            if (complaints.Count > 0 || complaints != null)
            {
                foreach (var complaint in complaints)
                {
                    var comResponse = new GetComplaintResponseDTO
                    {
                        Message = complaint.Message,
                        SenderName = complaint.User.FullName,
                        Date = complaint.CreatedAt
                    };
                    compResponse.Add(comResponse);
                }
            }

            response.Complaints = compResponse;
        }

        return response;

    }
}