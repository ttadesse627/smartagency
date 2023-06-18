


using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public record ShowOrderStatusQuery(Guid OrderId) : IRequest<ShowOrderStatusResponseDTO>
{ }
public class ShowOrderStatusQueryHandler : IRequestHandler<ShowOrderStatusQuery, ShowOrderStatusResponseDTO>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    public ShowOrderStatusQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository, IApplicantProcessRepository applicantProcessRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
        _applicantProcessRepository = applicantProcessRepository;
    }

    public async Task<ShowOrderStatusResponseDTO> Handle(ShowOrderStatusQuery request, CancellationToken cancellationToken)
    {
        var response = new ShowOrderStatusResponseDTO();
        var ordEagerLoadedProps = new string[] { "Employee", "Employee.MaritalStatus", "Employee.Religion", "Priority", "Partner", "Sponsor" };
        var applProcLoadedProps = new string[] { "Applicant", "ProcessDefinition" };
        var order = await _orderRepository.GetWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.Id == request.OrderId, ordEagerLoadedProps
                        );

        if (order.Employee != null)
        {
            var applicantFullName = order.Employee.FirstName + " " + order.Employee.MiddleName + " " + order.Employee.LastName;
            var applicantResponse = new ApplicantInfoResponseDTO
            {
                PassportNumber = order.Employee?.PassportNumber,
                FullName = applicantFullName,
                Sex = order.Employee?.Gender.ToString(),
                MaritalSatus = order.Employee?.MaritalStatus?.Value,
                Religion = order.Employee?.Religion?.Value
            };
            response.ApplicantInformation = applicantResponse;
        }

        var orderResponse = new OrderInfoResponseDTO
        {
            OrderNumber = order.OrderNumber,
            ClientName = order.Partner?.PartnerName,
            Priority = order.Priority?.Value,
            VisaNumber = order.VisaNumber,
            SponsorName = order.Sponsor?.FullName
        };
        response.OrderInformation = orderResponse;
        var applicantProcesses = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ApplicantId == order.EmployeeId, applProcLoadedProps);

        var statusInfo = new List<ProcessStatusResponseDTO>();
        var statusInformation = new StatusInfoResponseDTO();
        foreach (var applicantProcess in applicantProcesses)
        {
            var status = new ProcessStatusResponseDTO
            {
                StatusName = applicantProcess.ProcessDefinition?.Name,
                Date = applicantProcess.Date
            };
            statusInfo.Add(status);
        }
        statusInformation.ProcessStatuses = statusInfo;
        response.StatusInformation = statusInformation;
        return response;
    }
}