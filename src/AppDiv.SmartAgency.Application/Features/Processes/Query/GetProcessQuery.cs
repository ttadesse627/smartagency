
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Processes.Query;
public class GetProcessQuery : IRequest<ResponseContainerDTO<List<GetProcessResponseDTO>>> { }
public class GetProcessQueryHandler(IProcessRepository processRepository) : IRequestHandler<GetProcessQuery, ResponseContainerDTO<List<GetProcessResponseDTO>>>
{
    private readonly IProcessRepository _processRepository = processRepository;

    public async Task<ResponseContainerDTO<List<GetProcessResponseDTO>>> Handle(GetProcessQuery query, CancellationToken cancellationToken)
    {
        var response = new ResponseContainerDTO<List<GetProcessResponseDTO>>();
        var processRespList = new List<GetProcessResponseDTO>();
        var applProList = new List<ApplicantProcess>();

        var processes = await _processRepository.GetAllWithPredicateAsync(process => !process.Id.Equals(Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86")), "Country");

        if (processes.Count != 0)
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
                processRespList!.Add(processResponse);
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