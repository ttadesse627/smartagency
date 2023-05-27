

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public class GetForAssignmentQuery : IRequest<List<GetForAssignmentDTO>>
{ }
public class GetForAssignmentQueryHandler : IRequestHandler<GetForAssignmentQuery, List<GetForAssignmentDTO>>
{
    private readonly IApplicantRepository _applicantRepository;
    public GetForAssignmentQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<List<GetForAssignmentDTO>> Handle(GetForAssignmentQuery request, CancellationToken cancellationToken)
    {
        var applicantResponse = new List<GetForAssignmentDTO>();
        var eagerLoadedProperties = new string[]{"Order",  "Jobtitle","Language","Religion", "Salary"};
        var applicantList = await _applicantRepository.GetAllWithPredicateAsync
                        (
                            applicant => applicant.IsDeleted == false && applicant.Order == null, eagerLoadedProperties
                        );
        applicantResponse = CustomMapper.Mapper.Map(applicantList, applicantResponse);
        return applicantResponse;
    }
}