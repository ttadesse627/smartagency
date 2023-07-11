using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record StepbackProcessCommand(StepbackProcessRequest Request) : IRequest<ApplicantProcessResponseDTO> { }
public class StepbackProcessCommandHandler : IRequestHandler<StepbackProcessCommand, ApplicantProcessResponseDTO>
{
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    public StepbackProcessCommandHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantProcessRepository,
                                        IApplicantRepository applicantRepository, IProcessDefinitionRepository proDefRepository)
    {
        _processRepository = processRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
    }
    public async Task<ApplicantProcessResponseDTO> Handle(StepbackProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor");
        var currentPd = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessDefinitionId, "Process");

        var process = currentPd.Process;
        var processDefs = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == process.Id);
        var processDefinitions = processDefs.OrderBy(pd => pd.Step).ToList();

        var currentPdIndex = processDefinitions.FindIndex(pd => pd.Id == currentPd.Id);
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
            // delete the applicant process entity for the current process definition
            var pendingApplProcesses = applicant.ApplicantProcesses
                .Where(ap => ap.ProcessDefinitionId == currentPd.Id)
                .ToList();

            foreach (var pendingApplProcess in pendingApplProcesses)
            {
                pendingApplProcess.Status = ProcessStatus.Out;
            }

            var prevPd = processDefinitions[prevPdIndex];
            var applProc = await _applicantProcessRepository.GetWithPredicateAsync(applpr => applpr.ApplicantId == request.ApplicantId && applpr.ProcessDefinitionId == prevPd.Id && applpr.Status == ProcessStatus.Out);
            applProc.Status = ProcessStatus.In;

            // Create a new applicant process for the next process definition
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

        // Return all the applicants in each process definitions within that Process
        var applProLoadedProperties = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
        var applLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var response = new ApplicantProcessResponseDTO();

        var definitions = new List<GetProcessDefinitionResponseDTO>();

        var isInitialProcess = await _processRepository.GetMinStepProcessesAsync(process!.Id);

        if (isInitialProcess)
        {

            var processReadyApplicants = new List<GetApplProcessResponseDTO>();
            var readyApplicants = await _applicantRepository.GetAllWithPredicateAsync(app => app.ApplicantProcesses == null || app.ApplicantProcesses.Any() == false);
            if (readyApplicants != null && readyApplicants.Count > 0 && processDefs != null && processDefs.Count > 0)
            {
                var nextpdIds = new List<Guid>();
                var nxtPdStep = processDefs.OrderBy(p => p.Step).First().Step;
                nextpdIds.AddRange(processDefs.Where(p => p.Step == nxtPdStep).Select(p => p.Id));

                foreach (var apl in readyApplicants)
                {
                    processReadyApplicants.Add(new GetApplProcessResponseDTO()
                    {
                        Id = apl.Id,
                        PassportNumber = apl.PassportNumber,
                        FullName = apl.FirstName + " " + apl.MiddleName + " " + apl.LastName,
                        OrderNumber = apl.Order?.OrderNumber!,
                        SponsorName = apl.Order?.Sponsor?.FullName!
                    });
                }

                definitions.Add(new GetProcessDefinitionResponseDTO
                {
                    Name = "ProcessReadyApplicants",
                    NextPdIds = nextpdIds,
                    ApplicantProcesses = processReadyApplicants
                });
            }
        }

        var lastPds = processDefs.Where(p => p.Step == processDefs.Max(p => p.Step)).ToList();

        foreach (var pd in processDefs)
        {
            var nextpdIds = new List<Guid>();
            if (lastPds.Contains(pd))
            {
                var nextPrStep = pd.Process!.Step + 1;
                var nextPrs = await _processRepository.GetAllWithPredicateAsync(p => p.Step == nextPrStep);
                foreach (var nextPr in nextPrs)
                {
                    nextpdIds.Add(await _proDefRepository.GetMinStepAsync(nextPr.Id));
                }
            }
            else
            {
                nextpdIds.AddRange(processDefs.Where(p => p.Step == pd.Step + 1).Select(p => p.Id));
            }
            var proApps = await _applicantProcessRepository.GetAllWithPredicateAsync(applPr => applPr.Status == ProcessStatus.In && applPr.ProcessDefinitionId == pd.Id, applProLoadedProperties);
            var pdApplicants = new List<GetApplProcessResponseDTO>();
            foreach (var applicant1 in proApps)
            {
                pdApplicants.Add(new GetApplProcessResponseDTO()
                {
                    Id = applicant1.Applicant!.Id,
                    PassportNumber = applicant1.Applicant.PassportNumber,
                    FullName = applicant1.Applicant.FirstName + " " + applicant1.Applicant.MiddleName + " " + applicant1.Applicant.LastName,
                    OrderNumber = applicant1.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant1.Applicant.Order?.Sponsor?.FullName!
                });
            }
            definitions.Add(new GetProcessDefinitionResponseDTO()
            {
                Id = pd.Id,
                Name = pd.Name,
                Step = pd.Step,
                NextPdIds = nextpdIds,
                ApplicantProcesses = pdApplicants
            });
        }

        response.ProcessDefinitions = definitions;

        return response;
    }

}