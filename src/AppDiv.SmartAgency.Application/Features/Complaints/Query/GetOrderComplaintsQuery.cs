


using AppDiv.SmartAgency.Application.Contracts.DTOs.ComplaintDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Complaints.Query;
public record GetOrderComplaintsQuery(Guid ApplicantId) : IRequest<GetOrderComplaintsResponseDTO> { }
public class GetOrderComplaintsQueryHandler : IRequestHandler<GetOrderComplaintsQuery, GetOrderComplaintsResponseDTO>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IComplaintRepository _complaintRepository;
    public GetOrderComplaintsQueryHandler(IApplicantRepository applicantRepository, IComplaintRepository complaintRepository)
    {
        _applicantRepository = applicantRepository;
        _complaintRepository = complaintRepository;
    }

    public async Task<GetOrderComplaintsResponseDTO> Handle(GetOrderComplaintsQuery query, CancellationToken cancellationToken)
    {
        var response = new GetOrderComplaintsResponseDTO();
        var explLoadedProps = new string[] { "Order", "Order.Sponsor", "Order.Partner", "Complaints", "Complaints.User", "Address", "Order.Sponsor.Address" };
        var orderedApplicant = await _applicantRepository.GetWithPredicateAsync(applicant => applicant.Id == query.ApplicantId && applicant.OrderId != null, explLoadedProps);

        var employeeName = orderedApplicant.FirstName + " " + orderedApplicant.MiddleName + " " + orderedApplicant.LastName;
        var employeeInfo = new EmployeeInfoDTO
        {
            Id = orderedApplicant.Id,
            EmployeeName = employeeName,
            HouseNumber = orderedApplicant.Address?.HouseNumber,
            PhoneNumber = orderedApplicant.Address?.PhoneNumber,
            MobileNumber = orderedApplicant.Address?.Mobile
        };
        response.EmployeeInfo = employeeInfo;

        var sponsorInfo = new SponsorInfoDTO
        {
            OrderNumber = orderedApplicant.Order?.OrderNumber,
            CustomerName = orderedApplicant.Order?.Partner?.PartnerName,
            VisaNumber = orderedApplicant.Order?.VisaNumber,
            SponsorName = orderedApplicant.Order?.Sponsor?.FullName,
            HousePhone = orderedApplicant.Order?.Sponsor?.Address?.PhoneNumber,
            MobilePhone = orderedApplicant.Order?.Sponsor?.Address?.Mobile
        };

        response.SponsorInfo = sponsorInfo;

        var complaints = await _complaintRepository.GetAllWithPredicateAsync(comp => comp.ApplicantId == query.ApplicantId, "User");
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

        return response;
    }
}