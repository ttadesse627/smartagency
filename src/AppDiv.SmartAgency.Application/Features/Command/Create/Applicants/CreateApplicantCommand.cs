

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Applicants;
public record CreateApplicantCommand(CreateApplicantRequest applicantRequest) : IRequest<ServiceResponse<Int32>>
{

}

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
        // var createApplicantCommandValidator = new CreateApplicantCommandValidator(_applicantRepository);
        // var validationResult = await createApplicantCommandValidator.ValidateAsync(applicantRequest, cancellationToken);

        // if (validationResult.Errors.Count > 0)
        // {
        //     createApplicantResponse.Success = false;
        //     createApplicantResponse.Errors = new List<string>();
        //     foreach (var error in validationResult.Errors)
        //         createApplicantResponse.Errors.Add(error.ErrorMessage);
        //     createApplicantResponse.Message = createApplicantResponse.Errors[0];
        // }
        // if (createApplicantResponse.Success)
        // {
        var levelOfQualifications = new List<LookUp>();
        var qualificationTypes = new List<LookUp>();
        var awards = new List<LookUp>();
        var technicalSkills = new List<LookUp>();
        if (request.ApplicantEducation != null)
        {
            foreach (var levelOfQualificationId in request.ApplicantEducation.EducationLevelofQualifications!)
            {
                var levelOfQualification = await _lookUpRepository.GetByIdAsync(levelOfQualificationId);
                levelOfQualifications.Add(levelOfQualification);
            }
            foreach (var qualificationTypeId in request.ApplicantEducation.EducationQualificationTypes!)
            {
                var qualificationType = await _lookUpRepository.GetByIdAsync(qualificationTypeId);
                qualificationTypes.Add(qualificationType);
            }
            foreach (var awardId in request.ApplicantEducation.EducationAwards!)
            {
                var award = await _lookUpRepository.GetByIdAsync(awardId);
                awards.Add(award);
            }
        }
        if (request.ApplicantTechnicalSkills != null)
        {
            foreach (var technicalSkillId in request.ApplicantTechnicalSkills!)
            {
                var technicalSkill = await _lookUpRepository.GetByIdAsync(technicalSkillId);
                technicalSkills.Add(technicalSkill);
            }
        }
        var applicantEntity = CustomMapper.Mapper.Map<Applicant>(request);
        applicantEntity.ApplicantEducation!.EducationLevelofQualifications = levelOfQualifications;
        applicantEntity.ApplicantEducation.EducationQualificationTypes = qualificationTypes;
        applicantEntity.ApplicantEducation.EducationAwards = awards;
        applicantEntity.ApplicantTechnicalSkills = technicalSkills;
        int count =  await _applicantRepository.CreateApplicantAsync(applicantEntity);
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