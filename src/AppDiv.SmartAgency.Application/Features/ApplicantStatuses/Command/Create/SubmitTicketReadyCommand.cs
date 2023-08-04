using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTicketReadyCommand(SubmitTicketReadyRequest Request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTicketReadyCommandHandler : IRequestHandler<SubmitTicketReadyCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITicketReadyRepository _ticketReadyRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTicketReadyCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository, IMediator mediator,
    IProcessDefinitionRepository definitionRepository, ITicketReadyRepository ticketReadyRepository)
    {
        _mediator = mediator;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _definitionRepository = definitionRepository;
        _ticketReadyRepository = ticketReadyRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTicketReadyCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var response = new TicketProcessResponseDTO();

        var applPros = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "00fa1a8e-ac70-400e-8f37-20010f81a27a");
        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id));
        var ticketOffice = await _lookupRepository.GetWithPredicateAsync(lk => lk.Id == request.TicketOfficeId);
        var applicantStatuses = new List<ApplicantProcess>();
        var ticketReadyAppls = new List<TicketReady>();


        // Update the applicant status on the ApplicantProcess table
        if (applPros != null && applPros.Any())
        {
            foreach (var applPro in applPros)
            {
                applPro.Status = ProcessStatus.Out;
            }
        }


        var tickReg = await _definitionRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7");
        if (applicants != null && applicants.Any())
        {
            foreach (var applicant1 in applicants)
            {
                var applicantStatus = new ApplicantProcess
                {
                    Applicant = applicant1,
                    ProcessDefinition = tickReg,
                    Date = request.Date,
                    Status = ProcessStatus.In
                };
                applicantStatuses.Add(applicantStatus);

                var tickReadyAppl = new TicketReady
                {
                    DateInterval = request.DateInterval,
                    TicketOffice = ticketOffice,
                    Applicant = applicant1
                };
                ticketReadyAppls.Add(tickReadyAppl);
            }
        }

        await _applicantProcessRepository.InsertAsync(applicantStatuses, cancellationToken);
        await _ticketReadyRepository.InsertAsync(ticketReadyAppls, cancellationToken);


        try
        {
            bool appStatusSuccess = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            bool appTicketSuccess = await _ticketReadyRepository.SaveChangesAsync(cancellationToken);

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