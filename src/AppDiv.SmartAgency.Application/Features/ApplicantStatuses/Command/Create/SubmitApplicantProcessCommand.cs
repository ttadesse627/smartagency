
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

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor", "Enjaz");
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
            currentStatus = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ProcessDefinitionId == request.PdId && appPr.ApplicantId == request.ApplicantId && appPr.Status == ProcessStatus.In);
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
                        throw new System.ApplicationException(ex.Message);
                    }
                }
                else
                {
                    response = await _mediator.Send(new GetApplProcessQuery(currentPId));
                }
            }
            else
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
                    throw new System.ApplicationException(ex.Message);
                }

                // Return all the applicants in each process definitions within that Process
                response = await _mediator.Send(new GetApplProcessQuery(currentPId));
            }
        }
        else
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
                throw new System.ApplicationException(ex.Message);
            }

            // Return all the applicants in each process definitions within that Process
            response = await _mediator.Send(new GetApplProcessQuery(currentPId));
        }

        // var applProLoadedProperties = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
        // var applLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        // var response = new ApplicantProcessResponseDTO();

        // var definitions = new List<GetProcessDefinitionResponseDTO>();

        // var processDefinitions = await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == currentPId, "Process");
        // var isInitialProcess = await _processRepository.GetMinStepProcessesAsync(currentPId);

        // if (isInitialProcess)
        // {
        //     var processReadyApplicants = new List<GetApplProcessResponseDTO>();
        //     var readyApplicants = await _applicantRepository.GetAllWithPredicateAsync(app => app.ApplicantProcesses == null || !app.ApplicantProcesses.Any());
        //     if (readyApplicants != null && readyApplicants.Count > 0 && processDefinitions != null && processDefinitions.Any())
        //     {
        //         var nextpdId = new Guid();
        //         var nxtPdStep = processDefinitions.OrderBy(p => p.Step).First().Step;
        //         nextpdId = processDefinitions.Where(p => p.Step == nxtPdStep).Select(p => p.Id).FirstOrDefault();

        //         foreach (var apl in readyApplicants)
        //         {
        //             processReadyApplicants.Add(new GetApplProcessResponseDTO()
        //             {
        //                 ApplicantId = apl.Id,
        //                 PassportNumber = apl.PassportNumber,
        //                 FullName = apl.FirstName + " " + apl.MiddleName + " " + apl.LastName,
        //                 OrderNumber = apl.Order?.OrderNumber!,
        //                 SponsorName = apl.Order?.Sponsor?.FullName!
        //             });
        //         }

        //         definitions.Add(new GetProcessDefinitionResponseDTO
        //         {
        //             Name = "ProcessReadyApplicants",
        //             NextPdId = nextpdId,
        //             ApplicantProcesses = processReadyApplicants
        //         });
        //     }
        // }
        // var lastPds = processDefinitions.Where(p => p.Step == processDefinitions.Max(p => p.Step)).ToList();

        // foreach (var pd in processDefinitions)
        // {
        //     var nextpdId = new Guid();
        //     if (lastPds.Contains(pd))
        //     {
        //         var nextPrStep = pd.Process!.Step + 1;
        //         var nextPr = await _processRepository.GetWithPredicateAsync(p => p.Step == nextPrStep);
        //         if (nextPr != null)
        //         {
        //             nextpdId = await _definitionRepository.GetMinStepAsync(nextPr.Id);
        //         }
        //     }
        //     else
        //     {
        //         nextpdId = processDefinitions.Where(p => p.Step == pd.Step + 1).FirstOrDefault().Id;
        //     }
        //     var proApps = await _applicantProcessRepository.GetAllWithPredicateAsync(applPr => applPr.Status == ProcessStatus.In && applPr.ProcessDefinitionId == pd.Id, applProLoadedProperties);
        //     var pdApplicants = new List<GetApplProcessResponseDTO>();
        //     foreach (var applicant1 in proApps)
        //     {
        //         pdApplicants.Add(new GetApplProcessResponseDTO()
        //         {
        //             ApplicantId = applicant1.Applicant!.Id,
        //             PassportNumber = applicant1.Applicant.PassportNumber,
        //             FullName = applicant1.Applicant.FirstName + " " + applicant1.Applicant.MiddleName + " " + applicant1.Applicant.LastName,
        //             OrderNumber = applicant1.Applicant.Order?.OrderNumber!,
        //             SponsorName = applicant1.Applicant.Order?.Sponsor?.FullName!
        //         });
        //     }

        //     definitions.Add(new GetProcessDefinitionResponseDTO()
        //     {
        //         PdId = pd.Id,
        //         Name = pd.Name,
        //         Step = pd.Step,
        //         NextPdId = nextpdId,
        //         ApplicantProcesses = pdApplicants
        //     });
        // }

        // response.ProcessDefinitions = definitions;

        return response;
    }

    // public async 

}