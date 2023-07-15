

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Enjazs.Command.Create;
using AppDiv.SmartAgency.Application.Features.Orders.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Update;
public record EditOrderStatusCommand(EditOrderStatusRequest StatusRequest) : IRequest<ShowOrderStatusResponseDTO> { }
public class EditOrderStatusCommandHandler : IRequestHandler<EditOrderStatusCommand, ShowOrderStatusResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IProcessDefinitionRepository _processDefinitionRepository;
    private readonly IEnjazRepository _enjazRepository;
    private readonly ITicketReadyRepository _ticketReadyRepository;
    private readonly ITicketRegistrationRepository _ticketRegistrationRepository;
    private readonly IFileService _fileService;
    public EditOrderStatusCommandHandler(IMediator mediator, IApplicantProcessRepository applicantProcessRepository,
    IProcessDefinitionRepository processDefinitionRepository, IApplicantRepository applicantRepository, IEnjazRepository enjazRepository,
    ITicketReadyRepository ticketReadyRepository, ITicketRegistrationRepository ticketRegistrationRepository, IFileService fileService)
    {
        _mediator = mediator;
        _applicantProcessRepository = applicantProcessRepository;
        _processDefinitionRepository = processDefinitionRepository;
        _applicantRepository = applicantRepository;
        _enjazRepository = enjazRepository;
        _ticketReadyRepository = ticketReadyRepository;
        _ticketRegistrationRepository = ticketRegistrationRepository;
        _fileService = fileService;
    }
    public async Task<ShowOrderStatusResponseDTO> Handle(EditOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var response = new ShowOrderStatusResponseDTO();
        var request = command.StatusRequest;

        if (request != null)
        {
            if (request.ApplicantId != Guid.Empty)
            {
                var applicant = await _applicantRepository.GetAsync(request.ApplicantId);
                if (request.Statuses != null && request.Statuses.Any())
                {
                    var statuses = new List<ApplicantProcess>();
                    foreach (var status in request.Statuses)
                    {
                        var processDefinition = await _processDefinitionRepository.GetAsync(status.StatusId);
                        var applicantStatus = new ApplicantProcess
                        {
                            Applicant = applicant,
                            ProcessDefinition = processDefinition,
                            Date = status.Date,
                            Status = ProcessStatus.Out
                        };
                        statuses.Add(applicantStatus);
                    }
                    if (statuses.Any())
                    {
                        await _applicantProcessRepository.InsertAsync(statuses, cancellationToken);
                        try
                        {
                            var success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException();
                        }
                    }
                }
                if (request.Enjaz != null)
                {
                    if (request.Enjaz.ApplicationNumber != null && request.Enjaz.TransactionCode != null)
                    {
                        var enjaz = new Enjaz
                        {
                            ApplicantId = applicant.Id,
                            ApplicationNumber = request.Enjaz.ApplicationNumber,
                            TransactionCode = request.Enjaz.TransactionCode,
                        };
                        try
                        {
                            await _enjazRepository.InsertAsync(enjaz, cancellationToken);
                            await _enjazRepository.SaveChangesAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(ex.Message);
                        }
                    }

                }
                if (request.TravelInfo != null)
                {
                    var ticketReady = new TicketReady
                    {
                        TicketOfficeId = request.TravelInfo.TicketOfficeId,
                        Applicant = applicant
                    };

                    var ticketRegistration = new TicketRegistration
                    {
                        RegisteredDate = request.TravelInfo.RegisteredDate,
                        TicketNumber = request.TravelInfo.TicketNumber,
                        FlightDate = request.TravelInfo.FlightDate,
                        DepartureTime = request.TravelInfo.DepartureTime,
                        Transit = request.TravelInfo.Transit,
                        ArrivalTime = request.TravelInfo.ArrivalTime,
                        AirLineId = request.TravelInfo.AirLineId,
                        TicketPrice = request.TravelInfo.TicketPrice,
                        Remark = request.TravelInfo.Remark,
                        Applicant = applicant
                    };

                    try
                    {
                        await _ticketReadyRepository.InsertAsync(ticketReady, cancellationToken);
                        await _ticketRegistrationRepository.InsertAsync(ticketRegistration, cancellationToken);
                        await _ticketReadyRepository.SaveChangesAsync(cancellationToken);
                        await _ticketRegistrationRepository.SaveChangesAsync(cancellationToken);

                        if (!string.IsNullOrEmpty(request.TravelInfo.TicketFile))
                        {
                            var folderName = Path.Combine("Resources", "Ticket Files");
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            var fileName = applicant.Id.ToString();
                            await _fileService.UploadBase64FileAsync(request.TravelInfo.TicketFile, fileName, pathToSave, FileMode.Create);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(ex.Message);
                    }
                }
                response = await _mediator.Send(new ShowOrderStatusQuery(applicant.Id), cancellationToken);
            }
        }

        return response;
    }
}