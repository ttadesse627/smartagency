using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.Processes.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record StepbackTicketCommand(StepbackProcessRequest Request) : IRequest<TicketProcessResponseDTO> { }
public class StepbackTicketCommandHandler : IRequestHandler<StepbackTicketCommand, TicketProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    public StepbackTicketCommandHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantProcessRepository,
                                        IApplicantRepository applicantRepository, IProcessDefinitionRepository proDefRepository, IMediator mediator)
    {
        _mediator = mediator;
        _processRepository = processRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
    }
    public async Task<TicketProcessResponseDTO> Handle(StepbackTicketCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var response = new TicketProcessResponseDTO();

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor");
        var currentPd = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessDefinitionId, "Process");

        if (currentPd.Id.Equals(Guid.Parse("5b912c00-9df3-47a1-a525-410abf239616")))
        {
            var applProc = await _applicantProcessRepository.GetWithPredicateAsync(applpr => applpr.ApplicantId == request.ApplicantId && applpr.ProcessDefinitionId.Equals(Guid.Parse("4048b353-039d-41b6-8690-a9aaa2e679cf")) && applpr.Status == ProcessStatus.Out);
            if (applProc != null)
            {
                applProc.Status = ProcessStatus.In;
            }
            else
            {
                applProc = await _applicantProcessRepository.GetWithPredicateAsync(applpr => applpr.ApplicantId == request.ApplicantId && applpr.ProcessDefinitionId.Equals(Guid.Parse("1dc479ab-fe84-4ca8-828f-9a21de7434e7")) && applpr.Status == ProcessStatus.Out);
                if (applProc != null)
                {
                    applProc.Status = ProcessStatus.In;
                }
            }

            // Delete an applicant process for the current process definition
            _applicantProcessRepository.Delete(appl => appl.ApplicantId == request.ApplicantId && appl.ProcessDefinitionId == request.ProcessDefinitionId);

            try
            {
                await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }
        else
        {
            var process = currentPd.Process;
            var processDefs = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == process.Id);
            var proDefinitions = processDefs.OrderBy(pd => pd.Step).ToList();

            var currentPdIndex = proDefinitions.FindIndex(pd => pd.Id == currentPd.Id);
            var prevPdIndex = currentPdIndex - 1;

            if (prevPdIndex < 0)
            {
                // This is the first process definition for the current process,
                // move the applicant to the prevous process
                var prevProcess = await _processRepository.GetWithPredicateAsync(p => p.Step == process.Step - 1, "ProcessDefinitions");
                if (prevProcess != null)
                {
                    // Set the applicant's status to 'In' for the last process definition of the previous process
                    var lastPdOfPrevProcess = prevProcess.ProcessDefinitions?.OrderBy(pd => pd.Step).Last();
                    var applProc = await _applicantProcessRepository.GetWithPredicateAsync(applpr => applpr.ApplicantId == request.ApplicantId && applpr.ProcessDefinitionId == lastPdOfPrevProcess.Id && applpr.Status == ProcessStatus.Out);
                    applProc.Status = ProcessStatus.In;
                    _applicantProcessRepository.Delete(appl => appl.ApplicantId == request.ApplicantId && appl.ProcessDefinitionId == request.ProcessDefinitionId);

                }

                try
                {
                    await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
            else
            {
                // This is not the first process definition for the current process,


                var prevPd = proDefinitions[prevPdIndex];
                var applProc = await _applicantProcessRepository.GetWithPredicateAsync(applpr => applpr.ApplicantId == request.ApplicantId && applpr.ProcessDefinitionId == prevPd.Id && applpr.Status == ProcessStatus.Out);
                applProc.Status = ProcessStatus.In;

                // Delete the applicant process entity for the current process definition
                _applicantProcessRepository.Delete(appl => appl.ApplicantId == request.ApplicantId && appl.ProcessDefinitionId == request.ProcessDefinitionId);

                try
                {
                    await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {

                    throw new ApplicationException(ex.Message);
                }
            }
        }

        response = await _mediator.Send(new GetTicketProcessApplicantsQuery());

        return response;
    }

}