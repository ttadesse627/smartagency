
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantStatuses.Query;
public record GetApplProcessQuery(Guid ProcessId) : IRequest<List<GetProcessDefinitionResponseDTO>> { }
public class GetApplProcessQueryHandler : IRequestHandler<GetApplProcessQuery, List<GetProcessDefinitionResponseDTO>>
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
    public async Task<List<GetProcessDefinitionResponseDTO>> Handle(GetApplProcessQuery query, CancellationToken cancellationToken)
    {
        var applicantLoadedProperties = new string[] { "Order", "Order.Sponsor" };
        var pdLoadedProperties = new string[] { "ApplicantProcesses", "ApplicantProcesses.Applicant.Order" };

        var response = new List<GetProcessDefinitionResponseDTO>();

        if (query.ProcessId != null)
        {
            var applicantProcessList = new List<ApplicantProcess>();
            try
            {
                var pros = await _processRepository.GetAllWithPredicateAsync(null, "ProcessDefinitions");
                if (pros.Count > 0 || pros != null)
                {
                    var pro = pros.OrderBy(pr => pr.Step).ToList()[0];
                    var appls = await _applicantRepository.GetAllWithPredicateAsync(appl => appl.ApplicantProcesses == null || appl.ApplicantProcesses.Count == 0);

                    if (appls.Count > 0 && pro.ProcessDefinitions?.Count > 0)
                    {
                        var sortedPds = pro.ProcessDefinitions.OrderBy(pd => pd.Step).ToList();
                        var firstPd = sortedPds[0];
                        foreach (var appl in appls)
                        {
                            var applicantProcess = new ApplicantProcess
                            {
                                Applicant = appl,
                                ProcessDefinition = firstPd,
                                Date = appl.CreatedAt,
                                Status = ProcessStatus.In
                            };

                            applicantProcessList.Add(applicantProcess);
                        }
                    }
                }

                await _applicantProcessRepository.InsertAsync(applicantProcessList, cancellationToken);
                await _applicantProcessRepository.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            // var processEntity = await _processRepository.GetWithPredicateAsync(pro => pro.Id == query.ProcessId, "ProcessDefinitions");

            var onProcessApplicants = await _definitionRepository.GetAllWithPredicateAsync(
                pd => pd.ProcessId == query.ProcessId, pdLoadedProperties);

            foreach (var proDef in onProcessApplicants)
            {
                var applicantProcesses = await _applicantProcessRepository.GetAllWithPredicateAsync(ap => ap.ProcessDefinitionId == proDef.Id);
                var pdApplicants = new List<GetApplProcessResponseDTO>();
                foreach (var applicant in applicantProcesses)
                {
                    pdApplicants.Add(new GetApplProcessResponseDTO()
                    {
                        Id = applicant.Applicant.Id,
                        PassportNumber = applicant.Applicant.PassportNumber,
                        FullName = applicant.Applicant.FirstName + " " + applicant.Applicant.MiddleName + " " + applicant.Applicant.LastName,
                        OrderNumber = applicant.Applicant.Order?.OrderNumber!,
                        SponsorName = applicant.Applicant.Order?.Sponsor?.FullName!
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
        }
        return response;
    }
}