using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitTraveledApplCommand(SubmitTraveledApplRequest request) : IRequest<TicketProcessResponseDTO> { }
public class SubmitTraveledApplCommandHandler : IRequestHandler<SubmitTraveledApplCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ITraveledApplicantRepository _traveledApplicantRepository;
    private readonly ILookUpRepository _lookupRepository;

    public SubmitTraveledApplCommandHandler(IApplicantProcessRepository applicantProcessRepository,
    IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository, IMediator mediator,
    IProcessDefinitionRepository proDefRepository, ITraveledApplicantRepository traveledApplicantRepository)
    {
        _mediator = mediator;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
        _traveledApplicantRepository = traveledApplicantRepository;
        _lookupRepository = lookUpRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(SubmitTraveledApplCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new TicketProcessResponseDTO();

        var applPros = await _applicantProcessRepository.GetAllWithPredicateAsync(appPr => request.ApplicantIds.Contains(appPr.ApplicantId) && appPr.ProcessDefinitionId.ToString() == "5b912c00-9df3-47a1-a525-410abf239616", "Applicant");
        var applicants = await _applicantRepository.GetAllWithPredicateAsync(app => request.ApplicantIds.Contains(app.Id), "ApplicantProcesses", "Order.Sponsor");
        var traveld = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "6b912c00-9df3-47a1-a524-410abf239616");


        var applicantStatuses = new List<ApplicantProcess>();
        var traveldApplicants = new List<TraveledApplicant>();


        // Update the applicant status on the ApplicantProcess table
        if (applPros != null && applPros.Any())
        {
            foreach (var applPro in applPros)
            {
                applPro.Status = ProcessStatus.Out;
            }
        }


        // var tickReg = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id.ToString() == "1dc479ab-fe84-4ca8-828f-9a21de7434e7");
        if (applicants != null && applicants.Any())
        {
            foreach (var applicant1 in applicants)
            {
                var applicantStatus = new ApplicantProcess
                {
                    Applicant = applicant1,
                    ProcessDefinition = traveld,
                    Date = (DateTime)request.Date,
                    Status = ProcessStatus.In
                };
                applicantStatuses.Add(applicantStatus);

                var traveledApplicant = new TraveledApplicant
                {
                    Remark = request.Remark,
                    Applicant = applicant1
                };
                traveldApplicants.Add(traveledApplicant);
            }
        }

        await _applicantProcessRepository.InsertAsync(applicantStatuses, cancellationToken);
        await _traveledApplicantRepository.InsertAsync(traveldApplicants, cancellationToken);
        try
        {
            bool appStatusSuccess = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            bool appTicketSuccess = await _traveledApplicantRepository.SaveChangesAsync(cancellationToken);

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