using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;


namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest Request) : IRequest<ApplicantProcessResponseDTO> { }
public class ApplicantProcessCommandHandler(IApplicantProcessRepository applicantProcessRepository,
                                    IApplicantRepository applicantRepository, IProcessDefinitionRepository definitionRepository, IMediator mediator) : IRequestHandler<SubmitApplicantProcessCommand, ApplicantProcessResponseDTO>
{
    private readonly IMediator _mediator = mediator;
    private readonly IProcessDefinitionRepository _definitionRepository = definitionRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository = applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository = applicantRepository;

    public async Task<ApplicantProcessResponseDTO> Handle(SubmitApplicantProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var response = new ApplicantProcessResponseDTO();
        var applicants = new List<Applicant>();

        if (request.ApplicantIds != null && request.ApplicantIds.Count != 0)
        {
            applicants.AddRange(await _applicantRepository.GetByIdsAsync(request.ApplicantIds, app => !app.IsDeleted, "ApplicantProcesses", "Order.Sponsor", "Enjaz"));
        }

        var currentPdId = request.PdId;
        var currentPId = new Guid();
        var nextPd = new ProcessDefinition();
        var currentPd = new ProcessDefinition();
        var processDefs = new List<ProcessDefinition>();

        var currentStatuses = new List<ApplicantProcess>();
        var newStatuses = new List<ApplicantProcess>();
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
            if (request.ApplicantIds != null && request.ApplicantIds.Count != 0)
            {
                foreach (var applicantId in request.ApplicantIds)
                {
                    currentStatuses.Add(await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ProcessDefinitionId == request.PdId && appPr.ApplicantId == applicantId && appPr.Status == ProcessStatus.In));
                }
            }
        }
        else
        {
            currentPId = nextPd.ProcessId;
        }

        processDefs.AddRange(await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == currentPId, "Process"));

        var lastPds = processDefs.Where(pd => pd.Step.Equals(maxStepOfCurrentPds));
        if (lastPds.Contains(currentPd))
        {
            if (nextPd.Process!.EnjazRequired)
            {
                if (applicants.Count != 0)
                {
                    foreach (var applicant in applicants)
                    {
                        if (applicant.Enjaz != null)
                        {
                            var newAppStatus = new ApplicantProcess
                            {
                                Applicant = applicant,
                                ProcessDefinition = nextPd,
                                Date = request.Date,
                                Status = ProcessStatus.In
                            };
                            newStatuses.Add(newAppStatus);

                            // Update the status of the current applicant
                            var currentStatus = currentStatuses.Find(status => status.ApplicantId == applicant.Id);
                            if (currentStatus != null)
                            {
                                currentStatus.Status = ProcessStatus.Out;
                            }
                        }
                    }
                }

                try
                {
                    await _applicantProcessRepository.InsertAsync(newStatuses, cancellationToken);
                    await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }

            }
            else
            {
                if (applicants.Count != 0)
                {
                    foreach (var applicant in applicants)
                    {
                        if (applicant != null)
                        {
                            var newAppStatus = new ApplicantProcess
                            {
                                Applicant = applicant,
                                ProcessDefinition = nextPd,
                                Date = request.Date,
                                Status = ProcessStatus.In
                            };
                            newStatuses.Add(newAppStatus);
                        }
                    }
                }
                if (currentStatuses.Count != 0)
                {
                    foreach (var currentStatus in currentStatuses)
                    {
                        if (request.ApplicantIds != null && request.ApplicantIds.Count != 0)
                        {
                            if (request.ApplicantIds.Contains(currentStatus.ApplicantId))
                            {
                                // update the status of applicant process for the current process definition
                                if (currentStatus != null)
                                {
                                    currentStatus.Status = ProcessStatus.Out;
                                }
                            }
                        }

                    }
                }
                try
                {
                    await _applicantProcessRepository.InsertAsync(newStatuses, cancellationToken);
                    await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }
        else
        {
            if (applicants.Count != 0)
            {
                foreach (var applicant in applicants)
                {
                    if (applicant != null)
                    {
                        var newAppStatus = new ApplicantProcess
                        {
                            Applicant = applicant,
                            ProcessDefinition = nextPd,
                            Date = request.Date,
                            Status = ProcessStatus.In
                        };
                        newStatuses.Add(newAppStatus);
                    }
                }
            }
            if (currentStatuses.Count != 0)
            {
                foreach (var currentStatus in currentStatuses)
                {
                    if (request.ApplicantIds != null && request.ApplicantIds.Count != 0)
                    {
                        if (request.ApplicantIds.Contains(currentStatus.ApplicantId))
                        {
                            // update the status of applicant process for the current process definition
                            if (currentStatus != null)
                            {
                                currentStatus.Status = ProcessStatus.Out;
                            }
                        }
                    }

                }
            }
            try
            {
                await _applicantProcessRepository.InsertAsync(newStatuses, cancellationToken);
                await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        // Return all the applicants in each process definitions within that Process
        response = await _mediator.Send(new GetApplProcessQuery(currentPId), cancellationToken);

        return response;
    }

}