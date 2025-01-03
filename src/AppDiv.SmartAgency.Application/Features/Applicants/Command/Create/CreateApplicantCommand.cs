

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Create;
public record CreateApplicantCommand(CreateApplicantRequest ApplicantRequest) : IRequest<ServiceResponse<Int32>> { }
public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, ServiceResponse<Int32>>
{
    private readonly ISmartAgencyDbContext _context;
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILookUpRepository _lookUpRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;
    private readonly IUserRepository _userRepository;

    public CreateApplicantCommandHandler(ISmartAgencyDbContext context, IApplicantRepository applicantRepository,
    ILookUpRepository lookUpRepository, IAttachmentRepository attachmentRepository, IFileService fileService, IUserRepository userRepository)
    {
        _context = context;
        _applicantRepository = applicantRepository;
        _lookUpRepository = lookUpRepository;
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
        _userRepository = userRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateApplicantCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var exceptions = new List<Exception>();
        var request = command.ApplicantRequest;
        var validator = new CreateApplicantCommandValidator(_applicantRepository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            // response.Errors?.Add(validationResult?.Errors.FirstOrDefault().ErrorMessage);

            response.Success = false;
            response.Errors ??= new List<string>(); // Initialize the errors list if null

            foreach (var error in validationResult.Errors)
            {
                response.Errors.Add(error.ErrorMessage);
            }

            response.Message = "one or more validation error occurs";
            return response;
        }

        var applicantEntity = CustomMapper.Mapper.Map<Applicant>(request);
        var representativeEntity = CustomMapper.Mapper.Map<Representative>(request.Witness?.Representative);
        var witnessList = new List<Witness>();
        var experienceList = new List<Experience>();
        var beneficiaryList = new List<Beneficiary>();
        var userId = _context.GetCurrentUserId();

        if (!userId.Equals(Guid.Empty))
        {
            // var explLoadedProp = new Dictionary<string, NavigationPropertyType>
            // {"Partner", NavigationPropertyType.REFERENCE};
            var user = await _userRepository.GetAsync(userId);
            if (user?.PartnerId != null)
            {
                applicantEntity.PartnerId = user.PartnerId;
            }
        }

        foreach (var wtns in request.Witness?.Witnesses!)
        {
            var witnessEntity = CustomMapper.Mapper.Map<Witness>(wtns);
            witnessList.Add(witnessEntity);
        }

        foreach (var bnf in request.Beneficiary?.Beneficiaries!)
        {
            var beneficiaryEntity = CustomMapper.Mapper.Map<Beneficiary>(bnf);
            beneficiaryList.Add(beneficiaryEntity);
        }

        foreach (var exp in request.ApplicantExperience?.Experiences!)
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
                lvlqs.Add(new LevelOfQualification { LookUp = loq });
            }
            applicantEntity.Education!.LevelOfQualifications = lvlqs;
        }
        if (qualificationTypes.Count > 0)
        {
            var qts = new List<QualificationType>();
            foreach (var qt in qualificationTypes)
            {
                qts.Add(new QualificationType
                {
                    LookUp = qt
                });
            }
            applicantEntity.Education!.QualificationTypes = qts;
        }
        if (awards.Count > 0)
        {
            var awds = new List<Award>();
            foreach (var awd in awards)
            {
                awds.Add(new Award
                {
                    LookUp = awd
                });
            }
            applicantEntity.Education!.Awards = awds;
        }
        if (technicalSkills.Count > 0)
        {
            var skls = new List<Skill>();
            foreach (var sk in technicalSkills)
            {
                skls.Add(new Skill
                {
                    LookUp = sk
                });
            }
            applicantEntity.Skills = skls;
            applicantEntity.LanguageSkills = CustomMapper.Mapper.Map<List<LanguageSkill>>(request.Skill?.LanguageSkills);
        }

        var attachmentIds = new List<Guid>();
        if (request.Attachment != null)
        {
            applicantEntity.OrderId = request.Attachment.OrderId;
            if (request.Attachment?.AttachmentFiles != null && request.Attachment.AttachmentFiles.Count > 0)
            {
                foreach (var attch in request.Attachment.AttachmentFiles)
                {
                    if (attch.AttachmentId != null)
                    {
                        attachmentIds.Add((Guid)attch.AttachmentId);
                    }
                }
            }
        }
        var attachments = await _attachmentRepository.GetByIdsAsync(attachmentIds);
        if (attachments.Any())
        {
            applicantEntity.Attachments = attachments.ToList();
        }

        // Apply the update to the database
        if (exceptions.Count == 0)
        {
            await _applicantRepository.InsertAsync(applicantEntity, cancellationToken);
        }
        bool success = await _applicantRepository.SaveChangesAsync(cancellationToken);
        if (success && (request.Attachment?.AttachmentFiles?.Count > 0 && request.Attachment.AttachmentFiles != null))
        {
            var fileModels = new List<FileModel>();
            // save order attachment
            foreach (var attachment in request.Attachment.AttachmentFiles)
            {
                var attach = attachments.FirstOrDefault(att => att.Id == attachment.AttachmentId);
                var folderName = Path.Combine("Resources", attach!.Title!);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!string.IsNullOrEmpty(attachment.AttachmentFile))
                {
                    var fileModel = new FileModel
                    {
                        Base64String = attachment.AttachmentFile,
                        FileName = applicantEntity.Id.ToString(),
                        PathToSave = pathToSave,
                        FileMode = FileMode.Create
                    };
                    fileModels.Add(fileModel);
                }
            }
            var fileSaved = await _fileService.UpLoadMultipleFilesAsync(fileModels);
            if (!fileSaved)
            {
                response.Errors?.Add("Couldn't save order attachment.");
            }
            response.Message = "The applicant is successfully added.";
            response.Success = true;
        }
        else
        {
            response.Message = "Couldn't add the requested applicant.";
            response.Success = false;
        }

        return response;
    }
}