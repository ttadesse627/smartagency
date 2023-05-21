

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public record GetSingleApplicantQuery(Guid id) : IRequest<GetApplicantResponseDTO>
{}
public class GetSingleApplicantQueryHandler : IRequestHandler<GetSingleApplicantQuery, GetApplicantResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IApplicantRepository _applicantRepository;
    public GetSingleApplicantQueryHandler(IApplicantRepository applicantRepository, IMediator mediator)
    {
        _applicantRepository = applicantRepository;
        _mediator = mediator;
    }
    public async Task<GetApplicantResponseDTO> Handle(GetSingleApplicantQuery request, CancellationToken cancellationToken)
    {
        var applicant = await _applicantRepository.GetApplicantByIdWithAsync(request.id);
        var applicantResponse = CustomMapper.Mapper.Map<GetApplicantResponseDTO>(applicant);
        return applicantResponse;
    }
}
