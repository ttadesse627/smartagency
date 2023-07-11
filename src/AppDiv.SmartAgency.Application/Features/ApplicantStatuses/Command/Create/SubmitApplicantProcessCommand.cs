
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;


namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest Request) : IRequest<ApplicantProcessResponseDTO> { }
public class ApplicantProcessCommandHandler : IRequestHandler<SubmitApplicantProcessCommand, ApplicantProcessResponseDTO>
{
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    public ApplicantProcessCommandHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantProcessRepository,
                                        IApplicantRepository applicantRepository, IProcessDefinitionRepository definitionRepository)
    {
        _processRepository = processRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _definitionRepository = definitionRepository;
    }
    public async Task<ApplicantProcessResponseDTO> Handle(SubmitApplicantProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        // var response = new ApplicantProcessResponseDTO();

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor");
        var currentPdId = request.PdId;
        var nextPd = new ProcessDefinition();
        var currentPd = new ProcessDefinition();
        var processDefs = new List<ProcessDefinition>();

        var currentStatus = new ApplicantProcess();
        // var nextpdIds = new List<Guid>();

        var inValidId = currentPdId == Guid.Parse("00000000-0000-0000-0000-000000000000");
        if (!inValidId)
        {
            currentPd = await _definitionRepository.GetWithPredicateAsync(def => def.Id == currentPdId, "Process");
            currentStatus = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ProcessDefinitionId == request.PdId && appPr.ApplicantId == request.ApplicantId && appPr.Status == ProcessStatus.In);
        }

        if (!request.NextPdId.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000")))
        {
            nextPd = await _definitionRepository.GetWithPredicateAsync(pd => pd.Id.Equals(request.NextPdId), "Process");
        }

        var currentPId = new Guid();

        if (!inValidId)
        {
            currentPId = (Guid)currentPd.ProcessId;
        }

        else
        {
            currentPId = (Guid)nextPd.ProcessId;
        }

        processDefs.AddRange(await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == currentPId, "Process"));

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
        var applProLoadedProperties = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
        var applLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var response = new ApplicantProcessResponseDTO();

        var definitions = new List<GetProcessDefinitionResponseDTO>();

        var isInitialProcess = await _processRepository.GetMinStepProcessesAsync(currentPId);

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
                    nextpdIds.Add(await _definitionRepository.GetMinStepAsync(nextPr.Id));
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