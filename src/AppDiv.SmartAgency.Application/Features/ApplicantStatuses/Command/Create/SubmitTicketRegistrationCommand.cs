using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketRegistrationCommand(SubmitRegisteredTicketRequest Request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketRegistrationCommandHandler : IRequestHandler<SubmitTicketRegistrationCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketRegistrationRepository _ticketRegistrationRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketRegistrationCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository, IMediator mediator,
    IProcessDefinitionRepository proDefRepository, ITicketRegistrationRepository ticketRegistrationRepository)
    {
        _mediator = mediator;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
        _ticketRegistrationRepository = ticketRegistrationRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketRegistrationCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var response = new TicketProcessResponseDTO();

        var applPros = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7", "Applicant");
        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id), "Order.Sponsor");

        var airLine = await _lookupRepository.GetWithPredicateAsync(lk => lk.Id == request.AirLineId);


        // Update the applicant status on the ApplicantProcess table
        if (applPros != null && applPros.Any())
        {
            foreach (var applPro in applPros)
            {
                applPro.Status = ProcessStatus.Out;
            }
        }

        var tickRefund = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "2d9ef769-6d03-4406-9849-430ff9723778");
        var traveledPr = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "5b912c00-9df3-47a1-a525-410abf239616");
        var applicantStatuses = new List<ApplicantProcess>();
        var tickregApplicants = new List<TicketRegistration>();


        if (applicants != null && applicants.Any())
        {
            foreach (var applicant1 in applicants)
            {
                var applicantStatus = new ApplicantProcess
                {
                    Applicant = applicant1,
                    ProcessDefinition = tickRefund,
                    Date = request.Date,
                    Status = ProcessStatus.In
                };
                var applTraveledProcess = new ApplicantProcess
                {
                    Applicant = applicant1,
                    ProcessDefinition = traveledPr,
                    Date = request.Date,
                    Status = ProcessStatus.In
                };
                applicantStatuses.Add(applicantStatus);
                applicantStatuses.Add(applTraveledProcess);

                var tickRegAppl = new TicketRegistration
                {
                    TicketNumber = request.TicketNumber,
                    AirLine = airLine,
                    FlightDate = request.FlightDate,
                    DepartureTime = request.DepartureTime,
                    Transit = request.Transit,
                    ArrivalTime = request.ArrivalTime,
                    Remark = request.Remark,
                    Applicant = applicant1
                };
                tickregApplicants.Add(tickRegAppl);
            }
        }

        await _applicantProcessRepository.InsertAsync(applicantStatuses, cancellationToken);
        await _ticketRegistrationRepository.InsertAsync(tickregApplicants, cancellationToken);
        try
        {
            bool appStatusSuccess = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            bool appTicketSuccess = await _ticketRegistrationRepository.SaveChangesAsync(cancellationToken);

            if (appStatusSuccess || appTicketSuccess)
            {
                response = await _mediator.Send(new GetTicketProcessApplicantsQuery());
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException(ex.Message);
        }

        return response;
    }

}