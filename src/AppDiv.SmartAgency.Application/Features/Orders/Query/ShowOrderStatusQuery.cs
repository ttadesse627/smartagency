


using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public record ShowOrderStatusQuery(Guid OrderId) : IRequest<ShowOrderStatusResponseDTO>
{ }
public class ShowOrderStatusQueryHandler : IRequestHandler<ShowOrderStatusQuery, ShowOrderStatusResponseDTO>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly ITicketRegistrationRepository _ticketRegistrationRepository;
    private readonly ITicketReadyRepository _ticketReadyRepository;
    public ShowOrderStatusQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository, IApplicantProcessRepository applicantProcessRepository,
        ITicketRegistrationRepository ticketRegistrationRepository, ITicketReadyRepository ticketReadyRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
        _applicantProcessRepository = applicantProcessRepository;
    }

    public async Task<ShowOrderStatusResponseDTO> Handle(ShowOrderStatusQuery request, CancellationToken cancellationToken)
    {
        var response = new ShowOrderStatusResponseDTO();
        var applResponse = new List<ApplicantInfoResponseDTO>();
        var applicantProcesses = new List<ApplicantProcess>();
        var ordEagerLoadedProps = new string[] { "Employee", "Employee.MaritalStatus", "Employee.Religion", "Priority", "Partner", "Sponsor", "Enjaz" };
        var applProcLoadedProps = new string[] { "Applicant", "ProcessDefinition", "ProcessDefinition.Process" };
        var order = await _orderRepository.GetWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.Id == request.OrderId, ordEagerLoadedProps
                        );

        if (order.Employees != null && order.Employees?.Count > 0)
        {
            foreach (var employee in order.Employees)
            {
                var applicantFullName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName;
                var applicantResponse = new ApplicantInfoResponseDTO
                {
                    PassportNumber = employee.PassportNumber,
                    FullName = applicantFullName,
                    Sex = employee.Gender.ToString(),
                    MaritalSatus = employee.MaritalStatus?.Value,
                    Religion = employee.Religion?.Value
                };
                applResponse.Add(applicantResponse);

                await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ApplicantId == employee.Id, applProcLoadedProps);
            }
            response.ApplicantInformation = applResponse;
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

        var statusInfo = new List<ProcessStatusResponseDTO>();
        var statusInformation = new StatusInfoResponseDTO();

        var enjazResponse = new EnjazResponseDTO();
        if (order.Enjaz != null)
        {
            enjazResponse.ApplicationNumber = order.Enjaz.ApplicationNumber;
            enjazResponse.TransactionCode = order.Enjaz.TransactionCode;
        }

        foreach (var applicantProcess in applicantProcesses)
        {
            var status = new ProcessStatusResponseDTO();
            if (applicantProcess.ProcessDefinition.Process.EnjazRequired == true)
            {
                status.StatusName = applicantProcess.ProcessDefinition.Name;
                status.Date = applicantProcess.Date;
                status.EnjazResponse = enjazResponse;
            }
            else
            {
                status.StatusName = applicantProcess.ProcessDefinition.Name;
                status.Date = applicantProcess.Date;
            }
            statusInfo.Add(status);
        }
        statusInformation.ProcessStatuses = statusInfo;
        response.StatusInformation = statusInformation;

        // var tktLoadedProps = new string[] { "AirLine" };
        var ticketRegistration = await _ticketRegistrationRepository.GetWithPredicateAsync(tk => tk.ApplicantId == order.Employees!.First().Id, "AirLine");
        var ticketReady = await _ticketReadyRepository.GetWithPredicateAsync(tk => tk.ApplicantId == order.Employees!.First().Id, "TicketOffice");

        var travelInfo = new TravelInfoResponseDTO
        {
            RegisteredDate = ticketRegistration.RegiteredDate,
            TicketNumber = ticketRegistration.TicketNumber,
            FlightDate = ticketRegistration.FlightDate,
            DepartureTime = ticketRegistration.DepartureTime,
            Transit = ticketRegistration.Transit,
            ArrivalTime = ticketRegistration.ArrivalTime,
            AirLine = ticketRegistration.AirLine.Value,
            TicketOffice = ticketReady.TicketOffice.Value,
            TicketPrice = ticketRegistration.TicketPrice,
            Remark = ticketRegistration.Remark
        };
        response.TravelInformation = travelInfo;
        return response;
    }
}