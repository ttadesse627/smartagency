


using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Query;
public class GetAllComplaintsQuery : IRequest<List<GetAllComplaintsResponseDTO>> { }
public class GetAllComplaintsQueryHandler : IRequestHandler<GetAllComplaintsQuery, List<GetAllComplaintsResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public GetAllComplaintsQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<List<GetAllComplaintsResponseDTO>> Handle(GetAllComplaintsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<GetAllComplaintsResponseDTO>();
        var orderComplaints = await _applicantRepository.GetAllWithPredicateAsync(app => app.OrderId != null && app.Complaints != null && app.Complaints.Count > 0, "Order", "Order.Sponsor");

        foreach (var orderComplaint in orderComplaints)
        {
                var employeeName = orderComplaint.FirstName + " " + orderComplaint.MiddleName + " " + orderComplaint.LastName;
                var days = DateTime.Now - orderComplaint.CreatedAt;
                int numberOfDays = (int)days.TotalDays;
                var complResponse = new GetAllComplaintsResponseDTO
                {
                    Id = orderComplaint.Id,
                    SponsorName = orderComplaint.Order?.Sponsor?.FullName,
                    EmployeeName = employeeName,
                    Days = numberOfDays
                };

                response.Add(complResponse);
        }

        return response;

    }
}