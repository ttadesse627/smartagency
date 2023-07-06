
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
public record GetApplProcessQuery(Guid ProcessId) : IRequest<ApplicantProcessResponseDTO> { }
public class GetApplProcessQueryHandler : IRequestHandler<GetApplProcessQuery, ApplicantProcessResponseDTO>
{
    private readonly IProcessRepository _processRepository;
    private readonly IProcessDefinitionRepository _definitionRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;

    public GetApplProcessQueryHandler(IApplicantRepository applicantRepository, IProcessRepository processRepository, IProcessDefinitionRepository definitionRepository, IApplicantProcessRepository applicantProcessRepository)
    {
        _applicantRepository = applicantRepository;
        _processRepository = processRepository;
        _definitionRepository = definitionRepository;
        _applicantProcessRepository = applicantProcessRepository;
    }
    public async Task<ApplicantProcessResponseDTO> Handle(GetApplProcessQuery query, CancellationToken cancellationToken)
    {
        var applProLoadedProperties = new string[] { "Applicant", "Applicant.Order", "Applicant.Order.Sponsor" };
        var applLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var response = new ApplicantProcessResponseDTO();
        var definitions = new List<GetProcessDefinitionResponseDTO>();
        var isInitialProcess = await _processRepository.GetMinStepProcessesAsync(query.ProcessId);

        if (isInitialProcess)
        {
            var processReadyApplicants = new List<GetApplProcessResponseDTO>();
            var readyApplicants = await _applicantRepository.GetAllWithPredicateAsync(app => app.ApplicantProcesses == null || app.ApplicantProcesses.Any() == false);
            if (readyApplicants != null && readyApplicants.Count > 0)
            {
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
                response.ProcessReadyApplicants = processReadyApplicants;
            }
        }

        var processDefs = await _definitionRepository.GetAllWithPredicateAsync(pd => pd.ProcessId == query.ProcessId, "Process");
        var maxStep = processDefs.Max(p => p.Step);
        var lastPds = processDefs.Where(p => p.Step == maxStep).ToList();

        var nextpdIds = new List<Guid>();

        foreach (var pd in processDefs)
        {
            if (lastPds.Contains(pd))
            {
                var nextPrStep = pd.Process!.Step + 1;
                var nextPr = await _processRepository.GetWithPredicateAsync(p => p.Step == nextPrStep);
                nextpdIds = await _definitionRepository.GetMinStepAsync(nextPr.Id);
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