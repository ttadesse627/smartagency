using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record StepbackProcessCommand(StepbackProcessRequest request) : IRequest<ServiceResponse<ApplicantProcessResponseDTO>> { }
public class StepbackProcessCommandHandler : IRequestHandler<StepbackProcessCommand, ServiceResponse<ApplicantProcessResponseDTO>>
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
    public async Task<ServiceResponse<ApplicantProcessResponseDTO>> Handle(StepbackProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new ServiceResponse<ApplicantProcessResponseDTO>();

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, "ApplicantProcesses", "Order.Sponsor");
        var currentPd = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessDefinitionId, "Process");

        var process = currentPd.Process;
        var pDefs = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == process.Id);
        var processDefinitions = pDefs.OrderBy(pd => pd.Step).ToList();

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
                response.Success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
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
                response.Success = await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
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