

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Create;
public record CreateApplicantCommand(CreateApplicantRequest applicantRequest) : IRequest<ServiceResponse<Int32>>
{ }
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
        var exceptions = new List<Exception>();
        var request = applicantRequest.applicantRequest;

        var applicantEntity = CustomMapper.Mapper.Map<Applicant>(request);
        var representativeEntity = CustomMapper.Mapper.Map<Representative>(request.Witness.Representative);
        var witnessList = new List<Witness>();
        var experienceList = new List<Experience>();
        var beneficiaryList = new List<Beneficiary>();
        foreach (var wtns in request.Witness.Witnesses)
        {
            var witnessEntity = CustomMapper.Mapper.Map<Witness>(wtns);
            witnessList.Add(witnessEntity);
        }

        foreach (var bnf in request.Beneficiary.Beneficiaries)
        {
            var beneficiaryEntity = CustomMapper.Mapper.Map<Beneficiary>(bnf);
            beneficiaryList.Add(beneficiaryEntity);
        }

        foreach (var exp in request.ApplicantExperience.Experiences)
        {
            var expEntity = CustomMapper.Mapper.Map<Experience>(exp);
            experienceList.Add(expEntity);
        }
        applicantEntity.Witnesses = witnessList;
        applicantEntity.Representative = representativeEntity;
        applicantEntity.Experiences = experienceList;
        applicantEntity.Beneficiaries = beneficiaryList;



        ICollection<LookUp> levelOfQualifications = new List<LookUp>();
        ICollection<LookUp> qualificationTypes = new List<LookUp>();
        ICollection<LookUp> awards = new List<LookUp>();
        ICollection<LookUp> technicalSkills = new List<LookUp>();


        if (request.Education != null)
        {
            if (request.Education.QualificationTypes != null)
            {
                foreach (var qualificationTypeId in request.Education.QualificationTypes!)
                {
                    var qualificationType = await _lookUpRepository.GetAsync(qualificationTypeId);
                    if (qualificationType == null)
                    {
                        exceptions.Add(new Exception($"There is no skill instance from lookup with an id {qualificationTypeId}"));
                    }
                    else qualificationTypes.Add(qualificationType);
                }
            }

            if (request.Education.LevelofQualifications != null)
            {
                foreach (var levelOfQualificationId in request.Education.LevelofQualifications!)
                {
                    var levelOfQualification = await _lookUpRepository.GetAsync(levelOfQualificationId);
                    if (levelOfQualification == null)
                    {
                        exceptions.Add(new Exception($"There is no skill instance from lookup with an id {levelOfQualificationId}"));
                    }
                    else levelOfQualifications.Add(levelOfQualification);
                }
            }
            if (request.Education.Awards != null)
            {
                foreach (var awardId in request.Education.Awards!)
                {
                    var award = await _lookUpRepository.GetAsync(awardId);
                    if (award == null)
                    {
                        exceptions.Add(new Exception($"There is no award instance from lookup with an id {awardId}"));
                    }
                    else awards.Add(award);
                }
            }

        }
        if (request.Skill?.Skills != null)
        {
            foreach (var skillId in request.Skill.Skills!)
            {
                var skill = await _lookUpRepository.GetAsync(skillId);
                if (skill == null)
                {
                    exceptions.Add(new Exception($"There is no skill instance from lookup with an id {skillId}"));
                }
                else technicalSkills.Add(skill);
            }
        }

        if (levelOfQualifications.Count > 0)
        {
            var lvlqs = new List<LevelOfQualification>();
            foreach (var loq in levelOfQualifications)
            {
                var lvlq = new LevelOfQualification();
                lvlq.LookUp = loq;
                lvlqs.Add(lvlq);
            }
            applicantEntity.Education.LevelOfQualifications = lvlqs;
        }
        if (qualificationTypes.Count > 0)
        {
            var qts = new List<QualificationType>();
            foreach (var qt in qualificationTypes)
            {
                var quaqt = new QualificationType();
                quaqt.LookUp = qt;
                qts.Add(quaqt);
            }
            applicantEntity.Education.QualificationTypes = qts;
        }
        if (awards.Count > 0)
        {
            var awds = new List<Award>();
            foreach (var awd in awards)
            {
                var aw = new Award();
                aw.LookUp = awd;
                awds.Add(aw);
            }
            applicantEntity.Education.Awards = awds;
        }
        if (technicalSkills.Count > 0)
        {
            var skls = new List<Skill>();
            foreach (var sk in technicalSkills)
            {
                var skl = new Skill();
                skl.LookUp = sk;
                skls.Add(skl);
            }
            applicantEntity.Skills = skls;
            applicantEntity.LanguageSkills = CustomMapper.Mapper.Map<List<LanguageSkill>>(request.Skill?.LanguageSkills);
        }

        // Apply the update to the database
        if (exceptions.Count() == 0)
        {
            await _applicantRepository.InsertAsync(applicantEntity, cancellationToken);
        }
        bool success = await _applicantRepository.SaveChangesAsync(cancellationToken);
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