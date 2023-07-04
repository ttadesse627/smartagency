using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Queries;
public class GetUnassignedApplicantsQuery : IRequest<GetUnAssignedApplicantsDTO> { }
public class GetUnassignedApplicantsQueryHandler : IRequestHandler<GetUnassignedApplicantsQuery, GetUnAssignedApplicantsDTO>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetUnassignedApplicantsQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<GetUnAssignedApplicantsDTO> Handle(GetUnassignedApplicantsQuery request, CancellationToken cancellationToken)
    {
        var unassignedApplicantResponse = new GetUnAssignedApplicantsDTO();
        var applicantResponse = new List<GetApplForAssignmentDTO>();
        var applEagerLoadedProps = new string[] { "Order", "Language", "Religion" };
        var applicantList = await _applicantRepository.GetAllWithPredicateAsync
                        (
                            applicant => applicant.IsDeleted == false && applicant.OrderId == null, applEagerLoadedProps
                        );

        if (applicantList.Count > 0)
        {
            foreach (var applicant in applicantList)
            {
                TimeSpan? dateDiff = DateTime.Now - applicant.BirthDate;
                int age = dateDiff != null ? (int)(dateDiff.Value.TotalDays / 365.25) : 0;
                var applResponse = new GetApplForAssignmentDTO
                {
                    Id = applicant.Id,
                    FirstName = applicant.FirstName,
                    Age = age,
                    PassportNumber = applicant.PassportNumber,
                    Religion = applicant.Religion?.Value,
                    Language = applicant.Language?.Value
                };
                applicantResponse.Add(applResponse);
            }
        }
        unassignedApplicantResponse.UnAssignedApplicants = applicantResponse;

        return unassignedApplicantResponse;
    }
}