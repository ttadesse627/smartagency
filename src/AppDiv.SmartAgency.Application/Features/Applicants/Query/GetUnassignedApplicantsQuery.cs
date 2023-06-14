using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Queries;
public class GetUnassignedApplicantsQuery : IRequest<List<GetApplForAssignmentDTO>> { }
public class GetUnassignedApplicantsQueryHandler : IRequestHandler<GetUnassignedApplicantsQuery, List<GetApplForAssignmentDTO>>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetUnassignedApplicantsQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<List<GetApplForAssignmentDTO>> Handle(GetUnassignedApplicantsQuery request, CancellationToken cancellationToken)
    {
        var applicantResponse = new List<GetApplForAssignmentDTO>();
        var applEagerLoadedProps = new string[] { "Order", "Jobtitle", "Language", "Religion", "Salary" };
        var applicantList = await _applicantRepository.GetAllWithPredicateAsync
                        (
                            applicant => applicant.IsDeleted == false && applicant.Order == null, applEagerLoadedProps
                        );
        applicantResponse = CustomMapper.Mapper.Map(applicantList, applicantResponse);

        return applicantResponse;
    }
}