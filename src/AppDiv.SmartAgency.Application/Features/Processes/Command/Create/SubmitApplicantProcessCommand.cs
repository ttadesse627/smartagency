using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest request) : IRequest<ServiceResponse<ApplicantProcessResponseDTO>> { }
public class ApplicantProcessCommandHandler : IRequestHandler<SubmitApplicantProcessCommand, ServiceResponse<ApplicantProcessResponseDTO>>
{
    private readonly IProcessDefinitionRepository _proDefRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    public ApplicantProcessCommandHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantProcessRepository,
                                        IApplicantRepository applicantRepository, IProcessDefinitionRepository proDefRepository)
    {
        _processRepository = processRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
        _proDefRepository = proDefRepository;
    }
    public async Task<ServiceResponse<ApplicantProcessResponseDTO>> Handle(SubmitApplicantProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        // var response = new ServiceResponse<ApplicantProcessResponseDTO>();
        // var appLoadedProps = new string[]
        //                     {
        //                         "ApplicantProcesses", "ApplicantProcesses.Process", "ApplicantProcesses.Process.Process"
        //                     };
        // var pdLoadedProps = new string[] { "Process" };

        // var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, appLoadedProps);
        // var currentPd = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessId, "Process");
        // var proDef = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.Process!.Id == currentPd.Process!.Id, pdLoadedProps);

        // if (applicant.ApplicantProcesses == null || applicant.ApplicantProcesses.Count == 0)
        // {
        //     var applProList = new List<ApplicantProcess>();
        //     var processes = await _processRepository.GetAllWithPredicateAsync(pr => pr.Step == 1, "ProcessDefinitions");
        //     foreach (var process in processes)
        //     {
        //         foreach (var pd in process.ProcessDefinitions!)
        //         {
        //             if (pd.Step == currentPd.Step + 1)
        //             {
        //                 var applProcess = new ApplicantProcess
        //                 {
        //                     Applicant = applicant,
        //                     Process = pd,
        //                     Date = request.Date,
        //                     Status = ProcessStatus.In
        //                 };
        //                 applProList.Add(applProcess);
        //             }
        //         }
        //     }

        //     try
        //     {
        //         await _applicantProcessRepository.InsertAsync(applProList, cancellationToken);
        //         response.Success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
        //         if (response.Success)
        //         {
        //             response.Message = "Added Successfully!";
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Success = false;
        //         response.Errors?.Add(ex.Message);
        //         throw new ApplicationException(ex.Message);
        //     }
        // }

        // return response;


        var response = new ServiceResponse<ApplicantProcessResponseDTO>();

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses.Process.Process");
        var currentPd = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessDefinitionId, "Process");
        var currentApplProcess = await _applicantProcessRepository.GetAllWithPredicateAsync(applPr => applPr.ProcessDefinitionId == request.ProcessDefinitionId);
        var process = currentPd.Process;
        var pDefs = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == process.Id);
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
                response.Success = false;
                response.Errors?.Add("No next process found.");
                throw new BadRequestException(response.Errors.First());
            }

            // Set the applicant's status to 'In' for the first process definition of the next process
            var firstPdOfNextProcess = nextProcess.ProcessDefinitions.OrderBy(pd => pd.Step).First();
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
                response.Success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    // response.Data = new ApplicantProcessResponseDTO
                    // {
                    //     Id = applProcess.Id,
                    //     Status = applProcess.Status
                    // };
                    response.Message = "Added Successfully!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors?.Add(ex.Message);
                throw new System.ApplicationException(ex.Message);
            }
        }
        else
        {
            // This is not the last process definition for the current process,
            // update the status of any pending applicant processes for the current process definition
            var pendingApplProcesses = applicant.ApplicantProcesses
                .Where(ap => ap.ProcessDefinitionId == currentPd.Id && ap.Status == ProcessStatus.In)
                .ToList();

            foreach (var pendingApplProcess in pendingApplProcesses)
            {
                pendingApplProcess.Status = ProcessStatus.Out;
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
                response.Success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    // response.Data = new ApplicantProcessResponseDTO
                    // {
                    //     Id = applProcess.Id,
                    //     Status = applProcess.Status
                    // };
                    response.Message = "Added Successfully!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors?.Add(ex.Message);
                throw new System.ApplicationException(ex.Message);
            }
        }

        return response;
    }

}