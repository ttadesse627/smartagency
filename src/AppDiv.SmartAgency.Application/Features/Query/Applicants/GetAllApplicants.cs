using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.Applicants;
public class GetAllApplicants : IRequest<PaginatedList<ApplicantsResponseDTO>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public GetAllApplicants(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
public class GetAllApplicantsHandler : IRequestHandler<GetAllApplicants, PaginatedList<ApplicantsResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetAllApplicantsHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<PaginatedList<ApplicantsResponseDTO>> Handle(GetAllApplicants request, CancellationToken cancellationToken)
    {
        var applicantList = await _applicantRepository.GetAll();

        var paginatedListApplicant = new PaginatedList<Applicant>((IReadOnlyCollection<Applicant>)applicantList, applicantList.Count(), request.PageNumber, request.PageSize);
        var paginatedList = await PaginatedList<Applicant>.CreateAsync(applicantList, request.PageNumber, request.PageSize);
        var paginatedListResp = CustomMapper.Mapper.Map<PaginatedList<ApplicantsResponseDTO>>(paginatedList);

        return paginatedListResp;
    }
}