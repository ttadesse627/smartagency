

using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public class GetProcessQuery : IRequest<ResponseContainerDTO<List<GetProcessResponseDTO>>> { }
public class GetProcessQueryHandler : IRequestHandler<GetProcessQuery, ResponseContainerDTO<List<GetProcessResponseDTO>>>
{
    private readonly IProcessRepository _processRepository;
    private readonly IProcessDefinitionRepository _processDefRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IApplicantProcessRepository _applicantProcessRepository;
    public GetProcessQueryHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantProcessRepository,
    IProcessDefinitionRepository processDefRepository, IApplicantRepository applicantRepository)
    {
        _processRepository = processRepository;
        _processDefRepository = processDefRepository;
        _applicantProcessRepository = applicantProcessRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ResponseContainerDTO<List<GetProcessResponseDTO>>> Handle(GetProcessQuery query, CancellationToken cancellationToken)
    {
        var response = new ResponseContainerDTO<List<GetProcessResponseDTO>>();
        var processRespList = new List<GetProcessResponseDTO>();
        // var excLoadedProps = new string[] { "Country", "ProcessDefinitions" };
        var applProList = new List<ApplicantProcess>();

        var unprocessedAppls = await _applicantRepository.GetAllWithPredicateAsync(appl => (appl.ApplicantProcesses == null || appl.ApplicantProcesses.Count == 0) && !(appl.IsDeleted));
        var processes = await _processRepository.GetAllWithAsync("Country");

        // var minStep = processes.Min(pr => pr.Step);
        // var initPros = processes.Where(pro => pro.Step == minStep).ToList();
        // if (initPros != null && initPros.Count > 0)
        // {
        //     foreach (var pr in initPros)
        //     {
        //         var pds = await _processDefRepository.GetMinStepAsync(pr.Id);
        //         if (pds != null && pds.Count > 0)
        //         {
        //             foreach (var pd in pds)
        //             {
        //                 if (unprocessedAppls != null && unprocessedAppls.Count > 0)
        //                 {
        //                     foreach (var applicant in unprocessedAppls)
        //                     {
        //                         var appPro = new ApplicantProcess
        //                         {
        //                             Applicant = applicant,
        //                             ProcessDefinition = pd,
        //                             Status = ProcessStatus.In,
        //                             Date = DateTime.Today
        //                         };
        //                         applProList.Add(appPro);
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     try
        //     {
        //         await _applicantProcessRepository.InsertAsync(applProList, cancellationToken);
        //         await _applicantProcessRepository.SaveChangesAsync(cancellationToken);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new ApplicationException(ex.Message);
        //     }
        // }
        if (processes.Any())
        {
            foreach (var process in processes)
            {
                var processResponse = new GetProcessResponseDTO
                {
                    Id = process.Id,
                    Name = process.Name,
                    Step = process.Step,
                    Country = process.Country?.Value,
                    IsVisaRequired = process.VisaRequired,
                    EnjazRequired = process.EnjazRequired
                };
                processRespList.Add(processResponse);
                if (processRespList != null && processRespList.Count > 0)
                {
                    _ = processRespList.OrderBy(p => p.Step); // Change this to .Sort() to preserve performance.
                }
            }
            response.Items = processRespList;
        }
        return response;
    }
}