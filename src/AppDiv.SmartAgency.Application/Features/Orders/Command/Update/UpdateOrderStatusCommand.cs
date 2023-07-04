

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Command.Update;
public record UpdateOrderStatusCommand(SubmitOrderStatusRequest request) : IRequest<ShowOrderStatusResponseDTO> { }
public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ShowOrderStatusResponseDTO>
{
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly ITicketRegistrationRepository _tktRegistrationRepository;
    private readonly ITicketReadyRepository _tktReadyRepository;
    private readonly ILookUpRepository _lookUpRepository;
    private readonly IOrderRepository _orderRepository;
    public UpdateOrderStatusCommandHandler(IProcessDefinitionRepository definitionRepository, IApplicantProcessRepository applicantProcessRepository,
        ITicketRegistrationRepository tktRegistrationRepository, ITicketReadyRepository tktReadyRepository, ILookUpRepository lookUpRepository, IOrderRepository orderRepository)
    {
        _definitionRepository = definitionRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _tktRegistrationRepository = tktRegistrationRepository;
        _tktReadyRepository = tktReadyRepository;
        _lookUpRepository = lookUpRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ShowOrderStatusResponseDTO> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var response = new ShowOrderStatusResponseDTO();
        var travelInfoRequest = command.request.TravelInformation;
        var statusInfoRequest = command.request.StatusInformation;

        var ordEagerLoadedProps = new string[] { "Employees", "Employees.MaritalStatus", "Employees.Religion", "Priority", "Partner", "Sponsor", "Enjaz" };
        var applProcLoadedProps = new string[] { "Applicant", "ProcessDefinition", "ProcessDefinition.Process" };
        var order = await _orderRepository.GetWithPredicateAsync
                        (
                            order => order.IsDeleted == false && order.Id == command.request.OrderId, ordEagerLoadedProps
                        );

        // Check if the ticket is previously registered
        var registeredTicket = await _tktRegistrationRepository.GetWithPredicateAsync(appltkt => appltkt.ApplicantId == order.Employees!.First().Id, "AirLine");
        var airLine = await _lookUpRepository.GetAsync(travelInfoRequest.AirLineId);
        if (registeredTicket == null)
        {
            var registerTicket = new TicketRegistration
            {
                TicketNumber = travelInfoRequest.TicketNumber,
                FlightDate = travelInfoRequest.FlightDate,
                DepartureTime = travelInfoRequest.DepartureTime,
                Transit = travelInfoRequest.Transit,
                Remark = travelInfoRequest.Remark,
                TicketPrice = travelInfoRequest.TicketPrice,
                AirLineId = travelInfoRequest.AirLineId,
            };
            await _tktRegistrationRepository.InsertAsync(registerTicket, cancellationToken);
        }
        else
        {
            registeredTicket.TicketNumber = travelInfoRequest.TicketNumber;
            registeredTicket.FlightDate = travelInfoRequest.FlightDate;
            registeredTicket.DepartureTime = travelInfoRequest.DepartureTime;
            registeredTicket.Transit = travelInfoRequest.Transit;
            registeredTicket.Remark = travelInfoRequest.Remark;
            registeredTicket.TicketPrice = travelInfoRequest.TicketPrice;
            registeredTicket.AirLine = airLine;
        }
        // Check if the ticket is previously registered
        var prevTicketReady = await _tktReadyRepository.GetWithPredicateAsync(appltkt => appltkt.ApplicantId == order.Employees!.First().Id, "TicketOffice");

        if (prevTicketReady == null)
        {
            var ticketOffice = await _lookUpRepository.GetAsync(travelInfoRequest.TicketOfficeId);
            var tktReady = new TicketReady
            {
                TicketOffice = ticketOffice
            };
            await _tktReadyRepository.InsertAsync(tktReady, cancellationToken);
        }
        else
        {
            var ticketOffice = await _lookUpRepository.GetAsync(travelInfoRequest.TicketOfficeId);
            prevTicketReady.TicketOffice = ticketOffice;
        }

        try
        {
            await _tktRegistrationRepository.SaveChangesAsync(cancellationToken);
            await _tktReadyRepository.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }

        // Set all the necessary objects those need to be returned
        if (order.Employees != null && order.Employees.Count > 0)
        {
            foreach (var employee in order.Employees)
            {
                var applicantFullName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName;
                var applicantResponse = new ApplicantInfoResponseDTO
                {
                    PassportNumber = employee?.PassportNumber,
                    FullName = applicantFullName,
                    Sex = employee?.Gender.ToString(),
                    MaritalSatus = employee?.MaritalStatus?.Value,
                    Religion = employee?.Religion?.Value
                };
                response.ApplicantInformation?.Add(applicantResponse);
            }
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
        var applicantProcesses = await _applicantProcessRepository.GetAllWithPredicateAsync(applPro => applPro.ApplicantId == order.Employees!.First().Id, applProcLoadedProps);

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
        var ticketRegistration = await _tktRegistrationRepository.GetWithPredicateAsync(appltkt => appltkt.ApplicantId == order.Employees!.First().Id, "AirLine");
        var ticketReady = await _tktReadyRepository.GetWithPredicateAsync(appltkt => appltkt.ApplicantId == order.Employees!.First().Id, "TicketOffice");

        var travelInfo = new TravelInfoResponseDTO
        {
            RegisteredDate = ticketRegistration.RegisteredDate,
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