

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record CreateProcessCommand(CreateProcessRequest request) : IRequest<ServiceResponse<Int32>>
{

}
public class CreateProcessCommandHandler : IRequestHandler<CreateProcessCommand, ServiceResponse<Int32>>
{
    private readonly IProcessRepository _processRepository;
    private readonly IProcessDefinitionRepository _processDefinitionRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    private readonly IApplicantRepository _applicantRepository;
    public CreateProcessCommandHandler(IProcessRepository processRepository, IProcessDefinitionRepository processDefinitionRepository, IApplicantProcessRepository applicantProcessRepository, IApplicantRepository applicantRepository)
    {
        _processRepository = processRepository;
        _processDefinitionRepository = processDefinitionRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<int>> Handle(CreateProcessCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new ServiceResponse<Int32>();
        var process = CustomMapper.Mapper.Map<Process>(request);
        var maxStep = await _processRepository.GetMaximumStepAsync(pr => !(pr.Name.ToLower().Contains(("ticket"))));
        try
        {
            await _processRepository.InsertAsync(process, cancellationToken);
            response.Success = await _processRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                var pros = await _processRepository.GetAllWithPredicateAsync(pro => pro.Step == 1, "ProcessDefinitions");
                if (pros.Count > 0)
                {
                    var appls = await _applicantRepository.GetAllWithPredicateAsync(appl => appl.ApplicantProcesses == null || appl.ApplicantProcesses.Count == 0);
                    foreach (var pro in pros)
                    {
                        if (appls.Count > 0 && pro.ProcessDefinitions?.Count > 0)
                        {
                            foreach (var pd in pro.ProcessDefinitions)
                            {
                                foreach (var appl in appls)
                                {
                                    var applicantProcess = new ApplicantProcess
                                    {
                                        Applicant = appl,
                                        ProcessDefinition = pd,
                                        Date = null,
                                        Status = ProcessStatus.In
                                    };
                                }
                            }
                        }
                    }
                }
                response.Success = await _processRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Message = "Added Successfully!";
                    response.Success = true;
                }
            }
        }
        catch (Exception ex)
        {

            throw new ApplicationException(ex.Message);
        }

        if (maxStep < request.Step)
        {
            maxStep = request.Step;
        }
        var ticketProcess = await _processRepository.GetWithPredicateAsync(pro => pro.Name.ToLower().Contains("ticket"));
        ticketProcess.Step = maxStep + 1;
        try
        {
            await _processRepository.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception ex)
        {
            throw new ApplicationException($"Unknown error occurred while updating the {ticketProcess.Name}'s step.");
        }
        return response;
    }
}