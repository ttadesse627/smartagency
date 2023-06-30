


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
        var explLoadedProps = new string[] { "Employees", "Sponsor", "Partner", "Complaints", "Complaints.User", "Employees.Address", "Sponsor.Address" };
        var order = await _orderRepository.GetWithPredicateAsync(order => order.Id == request.OrderId, explLoadedProps);

        if (order != null)
        {
            if (order.Employees != null && order.Employees.Count > 0)
            {
                foreach (var employee in order.Employees)
                {
                    var employeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName;
                    var employeeInfo = new EmployeeInfoDTO
                    {
                        Id = employee.Id,
                        EmployeeName = employeeName,
                        HouseNumber = employee.Address?.HouseNumber,
                        PhoneNumber = employee.Address?.PhoneNumber,
                        MobileNumber = employee.Address?.Mobile
                    };
                    response.EmployeeInfo = employeeInfo;
                }

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