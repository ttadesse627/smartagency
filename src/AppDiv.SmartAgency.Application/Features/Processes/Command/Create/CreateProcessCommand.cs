

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Create;
public record CreateProcessCommand(CreateProcessRequest request) : IRequest<ServiceResponse<Int32>> { }
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

        var maxStep = await _processRepository.GetMaximumStepAsync(pr => !(pr.Name.ToLower().Contains("ticket")));
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