


using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Query;
public class GetAllComplaintsQuery : IRequest<List<GetAllComplaintsResponseDTO>> { }
public class GetAllComplaintsQueryHandler : IRequestHandler<GetAllComplaintsQuery, List<GetAllComplaintsResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;
    public GetAllComplaintsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<GetAllComplaintsResponseDTO>> Handle(GetAllComplaintsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<GetAllComplaintsResponseDTO>();
        var orderComplaints = await _orderRepository.GetAllWithPredicateAsync(order => order.Complaints.Count > 0 || order.Complaints != null, "Employee", "Sponsor");

        foreach (var orderComplaint in orderComplaints)
        {
            if (orderComplaint.Employees != null && orderComplaint.Employees.Count > 0)
            {
                var employeeName = orderComplaint.Employees.First().FirstName + " " + orderComplaint.Employees.First().MiddleName + " " + orderComplaint.Employees.First().LastName;
                var days = DateTime.Now - orderComplaint.CreatedAt;
                int numberOfDays = (int)days.TotalDays;
                var complResponse = new GetAllComplaintsResponseDTO
                {
                    Id = orderComplaint.Id,
                    SponsorName = orderComplaint.Sponsor.FullName,
                    EmployeeName = employeeName,
                    Days = numberOfDays
                };

                response.Add(complResponse);
            }
        }

        return response;

    }
}