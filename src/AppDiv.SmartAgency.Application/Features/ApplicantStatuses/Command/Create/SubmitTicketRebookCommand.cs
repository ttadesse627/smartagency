
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketRebookCommand(SubmitTicketRebookRequest Request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketRebookCommandHandler : IRequestHandler<SubmitTicketRebookCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketRebookRepository _ticketRebookRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketRebookCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository, IMediator mediator,
    IProcessDefinitionRepository proDefRepository, ITicketRebookRepository ticketRebookRepository)
    {
        _mediator = mediator;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
        _ticketRebookRepository = ticketRebookRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketRebookCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var response = new TicketProcessResponseDTO();

        var applPros = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "3048b353-039d-41b6-8690-a9aaa2e679cf");
        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id), "ApplicantProcesses", "Order.Sponsor");
        var tktRebook = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "4048b353-039d-41b6-8690-a9aaa2e679cf");


        var applicantStatuses = new List<ApplicantProcess>();
        var tickRebookApplicants = new List<TicketRebook>();


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
                    ProcessDefinition = tktRebook,
                    Date = (DateTime)request.Date,
                    Status = ProcessStatus.In
                };
                applicantStatuses.Add(applicantStatus);

                var tickRebook = new TicketRebook
                {
                    DateInterval = request.DateInterval,
                    Applicant = applicant1
                };
                tickRebookApplicants.Add(tickRebook);
            }
        }

        await _applicantProcessRepository.InsertAsync(applicantStatuses, cancellationToken);
        await _ticketRebookRepository.InsertAsync(tickRebookApplicants, cancellationToken);
        try
        {
            bool appStatusSuccess = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            bool appTicketSuccess = await _ticketRebookRepository.SaveChangesAsync(cancellationToken);

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