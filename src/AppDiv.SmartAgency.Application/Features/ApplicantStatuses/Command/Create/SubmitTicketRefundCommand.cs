using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketRefundCommand(SubmitTicketRefundRequest request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketRefundCommandHandler : IRequestHandler<SubmitTicketRefundCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketRefundRepository _ticketRefundRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketRefundCommandHandler
    (IApplicantProcessRepository applicantProcessRepository, IMediator mediator,
        IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository,
        IProcessDefinitionRepository proDefRepository, ITicketRefundRepository ticketRefundRepository)
    {
        _mediator = mediator;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
        _ticketRefundRepository = ticketRefundRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketRefundCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new TicketProcessResponseDTO();

        var applPros = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "2d9ef769-6d03-4406-9849-430ff9723778", "Applicant");
        var applProsTravels = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "5b912c00-9df3-47a1-a525-410abf239616", "Applicant");
        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id), "Order.Sponsor");
        var tickRebook = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "3048b353-039d-41b6-8690-a9aaa2e679cf");

        var applicantStatuses = new List<ApplicantProcess>();
        var tickRefundApplicants = new List<TicketRefund>();


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
                    ProcessDefinition = tickRebook,
                    Date = request.Date,
                    Status = ProcessStatus.In
                };
                applicantStatuses.Add(applicantStatus);

                var tickRefundApplicant = new TicketRefund
                {
                    DateInterval = request.DateInterval,
                    Applicant = applicant1
                };
                tickRefundApplicants.Add(tickRefundApplicant);
            }
        }

        // Delete the applicant status on the ApplicantProcess table for the travel ticket.
        if (applProsTravels != null && applProsTravels.Any())
        {
            foreach (var applProsTravel in applProsTravels)
            {
                _applicantProcessRepository.Delete(applProsTravel);
            }
        }

        await _applicantProcessRepository.InsertAsync(applicantStatuses, cancellationToken);
        await _ticketRefundRepository.InsertAsync(tickRefundApplicants, cancellationToken);
        try
        {
            bool appStatusSuccess = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            bool appTicketSuccess = await _ticketRefundRepository.SaveChangesAsync(cancellationToken);

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