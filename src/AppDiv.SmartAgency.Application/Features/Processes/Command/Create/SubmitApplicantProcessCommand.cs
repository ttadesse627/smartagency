using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest request) : IRequest<List<GetProcessDefinitionResponseDTO>> { }
public class ApplicantProcessCommandHandler : IRequestHandler<SubmitApplicantProcessCommand, List<GetProcessDefinitionResponseDTO>>
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
    public async Task<List<GetProcessDefinitionResponseDTO>> Handle(SubmitApplicantProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor");
        var currentPd = await _definitionRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessDefinitionId, "Process");

        var process = currentPd.Process;
        var pDefs = await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == process.Id);
        var processDefinitions = pDefs.OrderBy(pd => pd.Step).ToList();

        var currentPdIndex = processDefinitions.FindIndex(pd => pd.Id == currentPd.Id);
        var nextPdIndex = currentPdIndex + 1;

        if (nextPdIndex >= processDefinitions.Count)
        {
            // This is the last process definition for the current process,
            // move the applicant to the next process
            var nextProcess = await _processRepository.GetWithPredicateAsync(p => p.Step == process.Step + 1, "ProcessDefinitions");
            if (nextProcess == null)
            {
                throw new BadRequestException("Finished the process!");
            }

            // Set the applicant's status to 'In' for the first process definition of the next process
            var firstPdOfNextProcess = nextProcess.ProcessDefinitions?.OrderBy(pd => pd.Step).First();
            var applProcess = new ApplicantProcess
            {
                Applicant = applicant,
                ProcessDefinition = firstPdOfNextProcess,
                Date = request.Date,
                Status = ProcessStatus.In
            };

            try
            {
                await _applicantProcessRepository.InsertAsync(applProcess, cancellationToken);
                await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new System.ApplicationException(ex.Message);
            }
        }
        else
        {
            // This is not the last process definition for the current process,
            // update the status of applicant processes for the current process definition
            var updatedApplicantProcess = await _applicantProcessRepository.GetAllWithPredicateAsync(appPro => appPro.ApplicantId == applicant.Id && appPro.ProcessDefinitionId == currentPd.Id && appPro.Status == ProcessStatus.In);
            if (updatedApplicantProcess.Count > 0)
            {
                updatedApplicantProcess.First().Status = ProcessStatus.Out;
            }

            // Create a new applicant process for the next process definition
            var nextPd = processDefinitions[nextPdIndex];
            var applProcess = new ApplicantProcess
            {
                Applicant = applicant,
                ProcessDefinition = nextPd,
                Date = request.Date,
                Status = ProcessStatus.In
            };

            try
            {
                await _applicantProcessRepository.InsertAsync(applProcess, cancellationToken);
                var success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new System.ApplicationException(ex.Message);
            }
        }

        // Return all the applicants in each process definitions within that Process
        var applicantLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var pdLoadedProperties = new string[] { "ApplicantProcesses", "ApplicantProcesses.Applicant.Order" };

        var response = new List<GetProcessDefinitionResponseDTO>();

        var processEntity = await _processRepository.GetWithPredicateAsync(pro => pro.Id == currentPd.ProcessId, "ProcessDefinitions");

        var onProcessApplicants = await _definitionRepository.GetAllWithPredicateAsync(
            pd => pd.ApplicantProcesses.All(applPr => applPr.Status == ProcessStatus.In) && pd.ProcessId == process.Id, pdLoadedProperties);

        foreach (var proDef in onProcessApplicants)
        {
            var pdApplicants = new List<GetApplProcessResponseDTO>();
            foreach (var applicant1 in proDef.ApplicantProcesses)
            {
                pdApplicants.Add(new GetApplProcessResponseDTO()
                {
                    Id = applicant1.Applicant.Id,
                    PassportNumber = applicant1.Applicant.PassportNumber,
                    FullName = applicant1.Applicant.FirstName + " " + applicant1.Applicant.MiddleName + " " + applicant1.Applicant.LastName,
                    OrderNumber = applicant1.Applicant.Order?.OrderNumber!,
                    SponsorName = applicant1.Applicant.Order?.Sponsor?.FullName!
                });
            }
            response.Add(new GetProcessDefinitionResponseDTO()
            {
                Id = proDef.Id,
                Name = proDef.Name,
                Step = proDef.Step,
                ApplicantProcesses = pdApplicants
            });
        }

        return response;
    }

}