

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
    public UpdateOrderStatusCommandHandler(IProcessDefinitionRepository definitionRepository, IApplicantProcessRepository applicantProcessRepository,
        ITicketRegistrationRepository tktRegistrationRepository, ITicketReadyRepository tktReadyRepository)
    {
        _definitionRepository = definitionRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _tktRegistrationRepository = tktRegistrationRepository;
        _tktReadyRepository = tktReadyRepository;
    }

    public async Task<ShowOrderStatusResponseDTO> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var response = new ShowOrderStatusResponseDTO();
        var travelInfoRequest = command.request.TravelInformation;
        var statusInfoRequest = command.request.StatusInformation;
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

        return response;
    }

}