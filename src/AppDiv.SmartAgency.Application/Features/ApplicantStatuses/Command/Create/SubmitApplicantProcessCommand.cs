
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;


namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest Request) : IRequest<ApplicantProcessResponseDTO> { }
public class ApplicantProcessCommandHandler : IRequestHandler<SubmitApplicantProcessCommand, ApplicantProcessResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    public ApplicantProcessCommandHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantProcessRepository,
                                        IApplicantRepository applicantRepository, IProcessDefinitionRepository definitionRepository, IMediator mediator)
    {
        _processRepository = processRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _definitionRepository = definitionRepository;
        _mediator = mediator;
    }
    public async Task<ApplicantProcessResponseDTO> Handle(SubmitApplicantProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var response = new ApplicantProcessResponseDTO();

        var currentPdId = request.PdId;
        var currentPId = new Guid();
        var nextPd = new ProcessDefinition();
        var currentPd = new ProcessDefinition();
        var processDefs = new List<ProcessDefinition>();

        var currentStatus = new ApplicantProcess();
        var emptyId = Guid.Parse("00000000-0000-0000-0000-000000000000");
        var maxStepOfCurrentPds = 0;

        if (!request.NextPdId.Equals(emptyId))
        {
            nextPd = await _definitionRepository.GetWithPredicateAsync(pd => pd.Id.Equals(request.NextPdId), "Process");
        }

        if (!currentPdId.Equals(emptyId))
        {
            currentPd = await _definitionRepository.GetWithPredicateAsync(def => def.Id == currentPdId, "Process");
            currentPId = currentPd.ProcessId;
            maxStepOfCurrentPds = await _definitionRepository.GetMaxStepAsync(currentPId);

        }
        else
        {
            currentPId = nextPd.ProcessId;
        }

        processDefs.AddRange(await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == currentPId, "Process"));

        var lastPds = processDefs.Where(pd => pd.Step.Equals(maxStepOfCurrentPds));
        if (lastPds.Contains(currentPd))
        {
            if (nextPd.Process.EnjazRequired)
            {
                if (request.ApplicantIds != null && request.ApplicantIds.Any())
                {
                    foreach (var applicantId in request.ApplicantIds)
                    {
                        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == applicantId, "ApplicantProcesses", "Order.Sponsor", "Enjaz");
                        currentStatus = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ProcessDefinitionId == request.PdId && appPr.ApplicantId == applicantId && appPr.Status == ProcessStatus.In);
                        if (applicant.Enjaz != null)
                        {
                            // Set the applicant's status to 'In' for the next process definitions
                            var newAppStatus = new ApplicantProcess
                            {
                                Applicant = applicant,
                                ProcessDefinition = nextPd,
                                Date = request.Date,
                                Status = ProcessStatus.In
                            };

                            // update the status of applicant process for the current process definition
                            if (currentStatus != null)
                            {
                                currentStatus.Status = ProcessStatus.Out;
                            }
                            try
                            {
                                await _applicantProcessRepository.InsertAsync(newAppStatus, cancellationToken);
                                await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException(ex.Message);
                            }
                        }
                        else
                        {
                            response = await _mediator.Send(new GetApplProcessQuery(currentPId), cancellationToken);
                        }
                    }

                }

            }
            else
            {
                if (request.ApplicantIds != null && request.ApplicantIds.Any())
                {
                    foreach (var applicantId in request.ApplicantIds)
                    {
                        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == applicantId, "ApplicantProcesses", "Order.Sponsor", "Enjaz");
                        currentStatus = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ProcessDefinitionId == request.PdId && appPr.ApplicantId == applicantId && appPr.Status == ProcessStatus.In);

                        // Set the applicant's status to 'In' for the next process definitions
                        var newAppStatus = new ApplicantProcess
                        {
                            Applicant = applicant,
                            ProcessDefinition = nextPd,
                            Date = request.Date,
                            Status = ProcessStatus.In
                        };


                        // update the status of applicant process for the current process definition
                        if (currentStatus != null)
                        {
                            currentStatus.Status = ProcessStatus.Out;
                        }
                        try
                        {
                            await _applicantProcessRepository.InsertAsync(newAppStatus, cancellationToken);
                            await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(ex.Message);
                        }
                    }

                }

                // Return all the applicants in each process definitions within that Process
                response = await _mediator.Send(new GetApplProcessQuery(currentPId), cancellationToken);
            }
        }

        return response;
    }

}