
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public record GetApplProcessQuery(Guid Id) : IRequest<List<GetProcessDefinitionResponseDTO>> { }
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

        if (query.Id != null)
        {
            var applicantProcessList = new List<ApplicantProcess>();
            try
            {
                var pros = await _processRepository.GetAllWithPredicateAsync(pro => pro.Step == 1, "ProcessDefinitions");
                if (pros.Count > 0 || pros != null)
                {
                    var appls = await _applicantRepository.GetAllWithPredicateAsync(appl => appl.ApplicantProcesses == null || appl.ApplicantProcesses.Count == 0);
                    foreach (var pro in pros)
                    {
                        if (appls.Count > 0 && pro.ProcessDefinitions?.Count > 0)
                        {
                            foreach (var pd in pro.ProcessDefinitions)
                            {
                                if (pd.Step == 0)
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

                                        applicantProcessList.Add(applicantProcess);
                                    }
                                }
                            }
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


            var processEntity = await _processRepository.GetWithPredicateAsync(pro => pro.Id == query.Id, "ProcessDefinitions");

            var onProcessApplicants = await _definitionRepository.GetAllWithPredicateAsync(
                pd => pd.ApplicantProcesses.All(applPr => applPr.Status == ProcessStatus.In) && pd.ProcessId == query.Id, pdLoadedProperties);

            foreach (var proDef in onProcessApplicants)
            {
                var pdApplicants = new List<GetApplProcessResponseDTO>();
                foreach (var applicant in proDef.ApplicantProcesses)
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