using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Command.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest request) : IRequest<ApplicantProcessResponseDTO> { }
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
        var request = command.request;
        var response = new ApplicantProcessResponseDTO();

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor");
        var currentPdId = request.PdId;
        var nextPd = new ProcessDefinition();
        var currentPd = new ProcessDefinition();
        var processDefs = new List<ProcessDefinition>();
        var currentStatus = new ApplicantProcess();
        var inValidId = (currentPdId == Guid.Parse("00000000-0000-0000-0000-000000000000") || currentPdId == null);
        if (!inValidId)
        {
            currentPd = await _definitionRepository.GetWithPredicateAsync(def => def.Id == request.PdId, "Process");
            currentStatus = await _applicantProcessRepository.GetWithPredicateAsync(appPr => appPr.ProcessDefinitionId == request.PdId && appPr.ApplicantId == request.ApplicantId && appPr.Status == ProcessStatus.In);
        }

        if (request.NextPdId != Guid.Parse("00000000-0000-0000-0000-000000000000") && request.NextPdId != null)
        {
            nextPd = await _definitionRepository.GetWithPredicateAsync(pd => pd.Id == request.NextPdId, "Process");
        }

        var applProLoadedProperties = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
        var applLoadedProperties = new string[] { "Order", "Order.Sponsor" };


        var definitions = new List<GetProcessDefinitionResponseDTO>();
        processDefs.AddRange(await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == currentPd.ProcessId, "Process"));

        // Set the applicant's status to 'In' for the first process definition of the next process

        var applProcess = new ApplicantProcess
        {
            Applicant = applicant,
            ProcessDefinition = nextPd,
            Date = (DateTime)request.Date,
            Status = ProcessStatus.In
        };

        // update the status of applicant processes for the current process definition
        // var updatedApplicantProcess = await _applicantProcessRepository.GetWithPredicateAsync(appPro => appPro.ApplicantId == applicant.Id && appPro.ProcessDefinitionId == currentPd.Id && appPro.Status == ProcessStatus.In);
        if (currentStatus != null)
        {
            currentStatus.Status = ProcessStatus.Out;
        }

        try
        {
            await _applicantProcessRepository.InsertAsync(applProcess, cancellationToken);
            await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException(ex.Message);
        }



        // Return all the applicants in each process definitions within that Process
        var isInitialProcess = false;

        if (!inValidId)
        {
            isInitialProcess = await _processRepository.GetMinStepProcessesAsync(currentPd.ProcessId);
        }


        if (isInitialProcess)
        {
            var processReadyApplicants = new List<GetApplProcessResponseDTO>();
            var readyApplicants = await _applicantRepository.GetAllWithPredicateAsync(app => app.ApplicantProcesses == null || app.ApplicantProcesses.Any() == false);
            if (readyApplicants != null && readyApplicants.Count > 0)
            {
                var nxtPdId = new Guid();
                var nxtPd = processDefs.OrderBy(p => p.Step).FirstOrDefault();
                if (nxtPd != null)
                {
                    nxtPdId = nxtPd.Id;
                }

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
                    NextPdId = nxtPdId,
                    ApplicantProcesses = processReadyApplicants
                });
            }
        }

        var lastPds = processDefs.Where(p => p.Step == processDefs.Max(p => p.Step)).ToList();

        foreach (var pd in processDefs)
        {
            var nxtpdId = new Guid();
            if (lastPds.Contains(pd))
            {
                var nextPrStep = pd.Process!.Step + 1;
                var nextPr = await _processRepository.GetWithPredicateAsync(p => p.Step == nextPrStep);
                nxtpdId = await _definitionRepository.GetMinStepAsync(nextPr.Id);
            }
            else
            {
                nxtpdId = processDefs.Where(p => p.Step == pd.Step + 1).Select(p => p.Id).FirstOrDefault();
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
                NextPdId = nxtpdId,
                ApplicantProcesses = pdApplicants
            });
        }
        response.ProcessDefinitions = definitions;

        return response;
    }

}