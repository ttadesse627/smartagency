

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Create;
public record CreateApplicantCommand(CreateApplicantRequest applicantRequest) : IRequest<ServiceResponse<Int32>>
{}
public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILookUpRepository _lookUpRepository;
    public CreateApplicantCommandHandler(IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository)
    {
        _applicantRepository = applicantRepository;
        _lookUpRepository = lookUpRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateApplicantCommand applicantRequest, CancellationToken cancellationToken)
    {
        var createApplicantResponse = new ServiceResponse<Int32>();
        var request = applicantRequest.applicantRequest;

            var applicantEntity = CustomMapper.Mapper.Map<Applicant>(request);

            ICollection<LookUp> levelOfQualifications =  new List<LookUp>();
            ICollection<LookUp> qualificationTypes = new List<LookUp>();
            ICollection<LookUp> awards = new List<LookUp>();
            ICollection<LookUp> technicalSkills = new List<LookUp>();
            
            if (request.Education != null)
            {
                foreach (var levelOfQualificationId in request.Education.LevelofQualifications!)
                {
                    var levelOfQualification = await _lookUpRepository.GetAsync(levelOfQualificationId);
                    levelOfQualifications.Add(levelOfQualification);
                }
                foreach (var qualificationTypeId in request.Education.QualificationTypes!)
                {
                    var qualificationType = await _lookUpRepository.GetAsync(qualificationTypeId);
                    qualificationTypes.Add(qualificationType);
                }
                foreach (var awardId in request.Education.Awards!)
                {
                    var award = await _lookUpRepository.GetAsync(awardId);
                    awards.Add(award);
                }
            }
            if (request.Skills != null)
            {
                foreach (var technicalSkillId in request.Skills!)
                {
                    var technicalSkill = await _lookUpRepository.GetAsync(technicalSkillId);
                    technicalSkills.Add(technicalSkill);
                }
            }

            applicantEntity.Education.QualificationTypes = qualificationTypes;
            applicantEntity.Education.LevelOfQualifications = levelOfQualifications;
            applicantEntity.Education.Awards = awards;
            applicantEntity.Skills = technicalSkills;
            // var applicantEntity = CustomMapper.Mapper.Map<Applicant>(request);

        int count = 0;

        if (applicantEntity.Education.LevelOfQualifications !=null 
                && applicantEntity.Education.QualificationTypes !=null
                && applicantEntity.Education.Awards !=null
                && applicantEntity.Skills !=null)
            {
                count = await _applicantRepository.CreateApplicantAsync(applicantEntity, cancellationToken);
            }
            // bool success = _applicantRepository.SaveChanges();
            bool success = count >= 1;
            if (success)
            {
                createApplicantResponse.Message = "The applicant is successfully added.";
                createApplicantResponse.Success = true;
            }
            else
            {
                createApplicantResponse.Message = "Couldn't add the requested applicant.";
                createApplicantResponse.Success = false;
            }
        // }

        return createApplicantResponse;
    }
}