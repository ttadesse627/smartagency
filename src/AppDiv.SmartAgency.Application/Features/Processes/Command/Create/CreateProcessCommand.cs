

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
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
        try
        {
            await _processRepository.InsertAsync(process, cancellationToken);
            response.Success = await _processRepository.SaveChangesAsync(cancellationToken);
            response.Message = "Successfully created!";
        }
        catch (System.Exception ex)
        {
            response.Errors?.Add(ex.Message);
            throw new ApplicationException($"Unknown error occurred while inserting process data.");
        }
        var ticketProcess = await _processRepository.GetAsync(Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"));
        if (ticketProcess != null)
        {
            var maxStep = await _processRepository.GetMaximumStepAsync(pr => pr.Id != Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"));
            ticketProcess.Step = maxStep + 1;
            try
            {
                var success = await _processRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Errors?.Add(ex.Message);
                response.Errors?.Add($"An error occurre while updating {ticketProcess.Name}'s step.");
            }
        }

        return response;
    }
}