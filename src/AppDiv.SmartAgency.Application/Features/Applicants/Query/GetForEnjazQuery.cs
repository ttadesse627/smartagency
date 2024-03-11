

using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Enums;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public class GetForEnjazQuery : IRequest<List<DropdownEnjazResponseDTO>>
{ }
public class GetForEnjazQueryHandler : IRequestHandler<GetForEnjazQuery, List<DropdownEnjazResponseDTO>>

{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;

    public GetForEnjazQueryHandler(IProcessRepository processRepository, IApplicantRepository applicantRepository, IApplicantProcessRepository applicantProcessRepository)
    {
        _processRepository = processRepository;
        _applicantRepository = applicantRepository;
        _applicantProcessRepository = applicantProcessRepository;
    }

    public async Task<List<DropdownEnjazResponseDTO>> Handle(GetForEnjazQuery request, CancellationToken cancellationToken)
    {
        var response = new List<DropdownEnjazResponseDTO>();


        var enjazRequiredProcesses = await _processRepository.GetEnjazRequiredProcessesAsync();
        if (enjazRequiredProcesses == null || !enjazRequiredProcesses.Any())
        {
            return response;
        }

        var enjazMinStep = enjazRequiredProcesses.Min(pr => pr.Step);
        var prevProcesses = await _processRepository.GetPrevStepEnjazProcessesAsync(pr => pr.Step == enjazMinStep - 1);
        if (prevProcesses == null || !prevProcesses.Any())
        {
            return response;
        }

        var prevProcess = prevProcesses.First();
        var lastProcessDefinition = prevProcess.ProcessDefinitions?.OrderByDescending(p => p.Step).FirstOrDefault();
        if (lastProcessDefinition != null)
        {
            var eligibleApplicants = await _applicantRepository.GetAllWithPredicateAsync(
                appl => appl.OrderId != null
                    && appl.ApplicantProcesses != null
                    && appl.ApplicantProcesses.Any(applPro => applPro.ProcessDefinitionId == lastProcessDefinition.Id && applPro.Status == ProcessStatus.In)
                    && appl.Enjaz == null,
                new string[] { "Order", "Order.Sponsor", "Jobtitle", "Language", "Religion", "Enjaz", "ApplicantProcesses" });

            foreach (var applicant in eligibleApplicants)
            {
                var resp = new DropdownEnjazResponseDTO
                {
                    ApplicantId = applicant.Id,
                    OrderNumber = applicant.Order?.OrderNumber,
                    SponsorFullName = applicant.Order?.Sponsor?.FullName,
                    EmployeeProfession = applicant.Jobtitle?.Value,
                    EmployeeLanguage = applicant.Language?.Value,
                    PassportNumber = applicant.PassportNumber,
                    EmployeeFullName = applicant.FirstName + " " + applicant.MiddleName + " " + applicant.LastName
                };
                response.Add(resp);
            }
        }
        return response;
    }
}