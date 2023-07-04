

using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusResponseDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public class OrderStatusQuery : IRequest<ResponseContainerDTO<List<OrderStatusResponseDTO>>>
{ }
public class OrderStatusQueryHandler : IRequestHandler<OrderStatusQuery, ResponseContainerDTO<List<OrderStatusResponseDTO>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicantRepository _applicantRepository;
    public OrderStatusQueryHandler(IOrderRepository orderRepository, IApplicantRepository applicantRepository)
    {
        _orderRepository = orderRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<ResponseContainerDTO<List<OrderStatusResponseDTO>>> Handle(OrderStatusQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainerDTO<List<OrderStatusResponseDTO>>();
        response.Items = new List<OrderStatusResponseDTO>();

        var applEagerLoadedProps = new string[]
        {
            "Order", "Order.Sponsor","Order.Payment","Order.Partner", "Jobtitle", "Language", "Religion",
            "ApplicantProcesses", "ApplicantProcesses.ProcessDefinition",
            "Order.PortOfArrival","Order.Priority", "TicketRegistration","TraveledApplicant"
        };
        var appOrders = await _applicantRepository.GetAllWithPredicateAsync(app => app.IsDeleted == false && app.OrderId != null, applEagerLoadedProps);
        var statuses = new List<StatusResponseDTO>();
        if (appOrders != null && appOrders.Count > 0)
        {
            foreach (var appOrder in appOrders)
            {
                if (appOrder.ApplicantProcesses != null && appOrder.ApplicantProcesses.Count > 0)
                {
                    foreach (var status in appOrder.ApplicantProcesses)
                    {
                        var statusResponse = new StatusResponseDTO { StatusName = status.ProcessDefinition?.Name, Date = status.Date };
                        statuses.Add(statusResponse);
                    }
                }
                var travelStatus = false;
                if (appOrder.TraveledApplicant != null)
                {
                    if (appOrder.TraveledApplicant.ApplicantId != null)
                    {
                        travelStatus = true;
                    }
                }
                var orderStatusResponse = new OrderStatusResponseDTO
                {
                    EmployeeId = appOrder.Id,
                    OrderNumber = appOrder.Order?.OrderNumber,
                    ClientName = appOrder.Order?.Partner?.PartnerName,
                    VisaNumber = appOrder.Order?.VisaNumber,
                    EmployeeFullName = appOrder.FirstName + " " + appOrder.MiddleName + " " + appOrder.LastName,
                    PassportNumber = appOrder.PassportNumber,
                    SponsorName = appOrder.Order?.Sponsor!.FullName,
                    BrokerName = appOrder.BrokerName?.Value,
                    Days = 5,
                    NumberOfDays = 10,
                    Left = 20,
                    Amount = appOrder.Order.Payment!.TotalAmount,
                    PaidAmount = appOrder.Order.Payment.PaidAmount,
                    Priority = appOrder.Order.Priority?.Value,
                    Jobtitle = appOrder.Jobtitle?.Value,
                    PortOfArrival = appOrder.Order.PortOfArrival?.Value,
                    TicketNo = appOrder.TicketRegistration?.TicketNumber,
                    FlightDate = appOrder.TicketRegistration?.FlightDate,
                    DepartureTime = appOrder.TicketRegistration?.TicketNumber,
                    Transit = appOrder.TicketRegistration?.TicketNumber,
                    ArrivalTime = appOrder.TicketRegistration?.TicketNumber,
                    TravelStatus = travelStatus,
                };

                orderStatusResponse.Statuses = statuses;
                response.Items.Add(orderStatusResponse);
            }
        }

        return response;
    }
}