

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record SubmitApplicantProcessCommand(SubmitApplicantProcessRequest request) : IRequest<ServiceResponse<Int32>> { }
public class ApplicantProcessCommandHandler : IRequestHandler<SubmitApplicantProcessCommand, ServiceResponse<Int32>>
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
    public async Task<ServiceResponse<int>> Handle(SubmitApplicantProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new ServiceResponse<Int32>();
        var appLoadedProps = new string[]
                            {
                                "ApplicantProcesses", "ApplicantProcesses.Process", "ApplicantProcesses.Process.Process"
                            };
        var pdLoadedProps = new string[] { "Process" };

        var applicant = await _applicantRepository.GetWithPredicateAsync(app => app.Id == request.ApplicantId, appLoadedProps);
        var currentPd = await _proDefRepository.GetWithPredicateAsync(pd => pd.Id == request.ProcessId, "Process");
        var proDef = await _proDefRepository.GetAllWithPredicateAsync(pd => pd.Process!.Id == currentPd.Process!.Id, pdLoadedProps);

        if (applicant.ApplicantProcesses == null || applicant.ApplicantProcesses.Count == 0)
        {
            var applProList = new List<ApplicantProcess>();
            var processes = await _processRepository.GetAllWithPredicateAsync(pr => pr.Step == 1, "ProcessDefinitions");
            foreach (var process in processes)
            {
                foreach (var pd in process.ProcessDefinitions!)
                {
                    if (pd.Step == currentPd.Step + 1)
                    {
                        var applProcess = new ApplicantProcess
                        {
                            Applicant = applicant,
                            Process = pd,
                            Date = request.Date,
                            Status = ProcessStatus.In
                        };
                        applProList.Add(applProcess);
                    }
                }
            }

            try
            {
                await _applicantProcessRepository.InsertAsync(applProList, cancellationToken);
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
                throw new ApplicationException(ex.Message);
            }
        }

        return response;
    }
}