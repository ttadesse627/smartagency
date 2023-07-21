using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
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
        var applEagerLoadedProps = new string[] { "Order", "Language", "Religion", "CurrentNationality", "Jobtitle", "Salary" };
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

                var religionResp = new LookUpItemResponseDTO();
                var languageResp = new LookUpItemResponseDTO();
                var nationalityResp = new LookUpItemResponseDTO();
                var jobTitleResp = new LookUpItemResponseDTO();
                var salaryResp = new LookUpItemResponseDTO();
                var experienceResp = new LookUpItemResponseDTO();

                if (applicant.Religion != null)
                {
                    religionResp = CustomMapper.Mapper.Map<LookUpItemResponseDTO>(applicant.Religion);
                }
                if (applicant.Language != null)
                {
                    languageResp = CustomMapper.Mapper.Map<LookUpItemResponseDTO>(applicant.Language);
                }
                if (applicant.CurrentNationality != null)
                {
                    religionResp = CustomMapper.Mapper.Map<LookUpItemResponseDTO>(applicant.CurrentNationality);
                }
                if (applicant.Jobtitle != null)
                {
                    jobTitleResp = CustomMapper.Mapper.Map<LookUpItemResponseDTO>(applicant.Jobtitle);
                }
                if (applicant.Salary != null)
                {
                    salaryResp = CustomMapper.Mapper.Map<LookUpItemResponseDTO>(applicant.Salary);
                }
                if (applicant.Experience != null)
                {
                    experienceResp = CustomMapper.Mapper.Map<LookUpItemResponseDTO>(applicant.Experience);
                }
                var applResponse = new GetApplForAssignmentDTO
                {
                    Id = applicant.Id,
                    FirstName = applicant.FirstName,
                    Age = age,
                    PassportNumber = applicant.PassportNumber,
                    Religion = religionResp,
                    Language = languageResp,
                    Nationality = nationalityResp,
                    JobTitle = jobTitleResp,
                    Experience = experienceResp,
                    Salary = salaryResp
                };
                applicantResponse.Add(applResponse);
            }
        }
        unassignedApplicantResponse.UnAssignedApplicants = applicantResponse;

        return unassignedApplicantResponse;
    }
}