

using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public class GetProcessQuery : IRequest<ResponseContainerDTO<List<GetProcessResponseDTO>>> { }
public class GetProcessQueryHandler : IRequestHandler<GetProcessQuery, ResponseContainerDTO<List<GetProcessResponseDTO>>>
{
    private readonly IProcessRepository _processRepository;
    private readonly IApplicantProcessRepository _applicantRepository;
    public GetProcessQueryHandler(IProcessRepository processRepository, IApplicantProcessRepository applicantRepository)
    {
        _processRepository = processRepository;
        _applicantRepository = applicantRepository;
    }
    public async Task<ResponseContainerDTO<List<GetProcessResponseDTO>>> Handle(GetProcessQuery query, CancellationToken cancellationToken)
    {
        var response = new ResponseContainerDTO<List<GetProcessResponseDTO>>();
        var processRespList = new List<GetProcessResponseDTO>();
        // var excLoadedProps = new string[] { "Country", "ProcessDefinitions" };
        var processes = await _processRepository.GetAllWithAsync("Country");
        if (processes.Count() > 0)
        {
            foreach (var process in processes)
            {
                var processResponse = new GetProcessResponseDTO
                {
                    Id = process.Id,
                    Name = process.Name,
                    Country = process.Country?.Value,
                    IsVisaRequired = process.VisaRequired,
                    EnjazRequired = process.EnjazRequired
                };
                processRespList.Add(processResponse);
            }
            response.Items = processRespList;
        }
        return response;
    }
}