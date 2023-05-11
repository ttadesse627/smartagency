using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Applicants;
public class GetAllApplicants : IRequest<List<ApplicantsResponseDTO>>
{
}
public class GetAllApplicantsHandler : IRequestHandler<GetAllApplicants, List<ApplicantsResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetAllApplicantsHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<List<ApplicantsResponseDTO>> Handle(GetAllApplicants request, CancellationToken cancellationToken)
    {
        var applicantListResponse = await _applicantRepository.GetAll();
        var applicantResponse = CustomMapper.Mapper.Map<List<ApplicantsResponseDTO>>(applicantListResponse);
        return applicantResponse;
    }
}