

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
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
        var processes = await _processRepository.GetAllAsync();
        var maxStep = 1;
        if (processes.Count() > 1)
        {
            maxStep = processes.Where(pr => pr.Id != Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86")).Max(pr => pr.Step);
        }
        if (request.Step > maxStep + 1)
        {
            throw new BadRequestException("The process steps should be increased by one.");
        }
        if (maxStep < request.Step)
        {
            maxStep = request.Step;
        }
        var ticketProcess = processes.FirstOrDefault(pr => pr.Id == Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"));
        ticketProcess.Step = maxStep + 1;
        try
        {
            await _processRepository.InsertAsync(process, cancellationToken);
            await _processRepository.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception ex)
        {
            throw new ApplicationException($"Unknown error occurred while updating the {ticketProcess.Name}'s step.");
        }
        return response;
    }
}