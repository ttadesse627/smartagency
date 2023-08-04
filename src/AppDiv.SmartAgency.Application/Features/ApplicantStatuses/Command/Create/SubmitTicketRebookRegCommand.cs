using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketRebookRegCommand(SubmitRegisteredTicketRequest request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketRebookRegCommandHandler : IRequestHandler<SubmitTicketRebookRegCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketRegistrationRepository _ticketRegistrationRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketRebookRegCommandHandler(IApplicantProcessRepository applicantProcessRepository,
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
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketRebookRegCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new TicketProcessResponseDTO();
        var airLine = await _lookupRepository.GetWithPredicateAsync(lk => lk.Id == request.AirLineId);

        var applPros = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "4048b353-039d-41b6-8690-a9aaa2e679cf", "Applicant");
        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id), "ApplicantProcesses", "Order.Sponsor");
        var registeredApplicants = await _ticketRegistrationRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id));
        var travel = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "5b912c00-9df3-47a1-a525-410abf239616");

        var applicantStatuses = new List<ApplicantProcess>();


        // Update the applicant status on the ApplicantProcess table
        if (applPros != null && applPros.Any())
        {
            foreach (var applPro in applPros)
            {
                applPro.Status = ProcessStatus.Out;
            }
        }

        if (applicants != null && applicants.Any())
        {
            foreach (var applicant1 in applicants)
            {
                var applicantStatus = new ApplicantProcess
                {
                    Applicant = applicant1,
                    ProcessDefinition = travel,
                    Date = (DateTime)request.Date,
                    Status = ProcessStatus.In
                };
                applicantStatuses.Add(applicantStatus);
            }
        }

        if (registeredApplicants != null && registeredApplicants.Any())
        {
            foreach (var registeredTicket in registeredApplicants)
            {
                registeredTicket.TicketNumber = request.TicketNumber;
                registeredTicket.AirLine = airLine;
                registeredTicket.FlightDate = request.FlightDate;
                registeredTicket.DepartureTime = request.DepartureTime;
                registeredTicket.Transit = request.Transit;
                registeredTicket.ArrivalTime = request.ArrivalTime;
                registeredTicket.Remark = request.Remark;
                registeredTicket.Applicant = applicants?.Where(appl => appl.Id == registeredTicket.ApplicantId).FirstOrDefault();
            }
        }

        await _applicantProcessRepository.InsertAsync(applicantStatuses, cancellationToken);
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