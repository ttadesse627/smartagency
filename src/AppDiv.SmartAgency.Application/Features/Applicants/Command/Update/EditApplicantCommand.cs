
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.EditApplicantRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public record EditApplicantCommand(EditApplicantRequest request) : IRequest<ServiceResponse<Int32>>
{ }
public class EditApplicantCommandHandler : IRequestHandler<EditApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILookUpRepository _lookUpRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;
    public EditApplicantCommandHandler(IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository, IAttachmentRepository attachmentRepository, IFileService fileService)
    {
        _applicantRepository = applicantRepository;
        _lookUpRepository = lookUpRepository;
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
    }

    public async Task<ServiceResponse<Int32>> Handle(EditApplicantCommand command, CancellationToken cancellationToken)
    {
        var serviceResponse = new ServiceResponse<Int32>();
        var exceptions = new List<Exception>();

        var eagerLoadedProperties = new string[]
                                    {
                                        "Skills.LookUp","Education.QualificationTypes.LookUp",
                                        "Education.LevelOfQualifications.LookUp","Education.Awards.LookUp"
                                    };
        var applicantEntity = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == command.request.Id, eagerLoadedProperties);

        if (applicantEntity != null)
        {
            CustomMapper.Mapper.Map(command.request, applicantEntity);

            ICollection<LookUp> qualificationTypesLookup = new List<LookUp>();
            ICollection<LookUp> levelOfQualificationsLookup = new List<LookUp>();
            ICollection<LookUp> awardsLookup = new List<LookUp>();
            ICollection<LookUp> skillsLookup = new List<LookUp>();

            foreach (var qualificationTypeId in command.request.Education?.QualificationTypes!)
            {
                var qt = await _lookUpRepository.GetAsync(qualificationTypeId);
                qualificationTypesLookup.Add(qt);
            }
            foreach (var levelOfQualificationId in command.request.Education.LevelofQualifications!)
            {
                var loq = await _lookUpRepository.GetAsync(levelOfQualificationId);
                levelOfQualificationsLookup.Add(loq);
            }
            foreach (var awardId in command.request.Education.Awards!)
            {
                var awd = await _lookUpRepository.GetAsync(awardId);
                awardsLookup.Add(awd);
            }
            foreach (var skill in command.request.Skills!)
            {
                var sk = await _lookUpRepository.GetAsync(skill);
                skillsLookup.Add(sk);
            }

            // Update the QualificationTypes
            if (command.request.Education != null && command.request.Education.QualificationTypes != null)
            {
                foreach (var qualificationTypeId in command.request.Education.QualificationTypes)
                {
                    var qt = await _lookUpRepository.GetAsync(qualificationTypeId);
                    if (qt != null)
                    {
                        // Check if the QualificationType already exists
                        var existingQualificationType = applicantEntity.Education?.QualificationTypes?
                            .FirstOrDefault(qt => qt.LookUp?.Id == qualificationTypeId);

                        if (existingQualificationType == null)
                        {
                            // Create a new QualificationType and add it to the list in the parent entity
                            var newQualificationType = new QualificationType
                            {
                                LookUp = qt
                            };
                            applicantEntity.Education?.QualificationTypes?.Add(newQualificationType);
                        }
                        else
                        {
                            exceptions.Add(new BadRequestException($"The Applicant is already up-to-date with a QualificationType with Id {qualificationTypeId}"));
                        }
                    }
                }
            }

            // Update the LevelOfQualifications
            if (command.request.Education != null && command.request.Education.LevelofQualifications != null)
            {
                foreach (var levelOfQualificationId in command.request.Education.LevelofQualifications)
                {
                    var loq = await _lookUpRepository.GetAsync(levelOfQualificationId);
                    if (loq != null)
                    {
                        // Check if the LevelOfQualification already exists
                        var existingLevelOfQualification = applicantEntity.Education?.LevelOfQualifications?
                            .FirstOrDefault(loq => loq.LookUp?.Id == levelOfQualificationId);

                        if (existingLevelOfQualification == null)
                        {
                            // Create a new LevelOfQualification and add it to the list in the parent entity
                            var newLevelOfQualification = new LevelOfQualification
                            {
                                LookUp = loq
                            };
                            applicantEntity.Education?.LevelOfQualifications?.Add(newLevelOfQualification);
                        }
                        else
                        {
                            exceptions.Add(new BadRequestException($"The Applicant is already up-to-date with a LevelOfQualification with Id {levelOfQualificationId}"));
                        }
                    }
                }
            }

            // Update the Awards
            if (command.request.Education != null && command.request.Education.Awards != null)
            {
                foreach (var awardId in command.request.Education.Awards)
                {
                    var award = await _lookUpRepository.GetAsync(awardId);
                    if (award != null)
                    {
                        // Check if the Award already exists
                        var existingAward = applicantEntity.Education?.Awards?
                            .FirstOrDefault(a => a.LookUp?.Id == awardId);

                        if (existingAward == null)
                        {
                            // Create a new Award and add it to the list in the parent entity
                            var newAward = new Award
                            {
                                LookUp = award
                            };
                            applicantEntity.Education?.Awards?.Add(newAward);
                        }
                        else
                        {
                            exceptions.Add(new BadRequestException($"The Applicant is already up-to-date with an Award with Id {award.Id}"));
                        }
                    }
                }
            }

            // Update the Skills
            if (command.request.Skills != null)
            {
                foreach (var skillId in command.request.Skills)
                {
                    var skill = await _lookUpRepository.GetAsync(skillId);
                    if (skill != null)
                    {
                        // Check if the Skill already exists
                        var existingSkill = applicantEntity.Skills?
                            .FirstOrDefault(s => s.LookUp?.Id == skillId);

                        if (existingSkill == null)
                        {
                            // Create a new Skill and add it to the list in the parent entity
                            var newSkill = new Skill
                            {
                                LookUp = skill
                            };
                            applicantEntity.Skills?.Add(newSkill);
                        }
                        else
                        {
                            exceptions.Add(new BadRequestException($"The Applicant is already up-to-date with a Skill with Id {skill.Id}"));
                        }
                    }
                }
            }

            // Save changes to the database
            try
            {
                serviceResponse.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
                if (serviceResponse.Success && (command.request.AttachmentFiles?.Count > 0 && command.request.AttachmentFiles != null))
                {
                    foreach (var attachment in command.request.AttachmentFiles)
                    {
                        // save applicant attachment
                        var file = attachment.AttachmentFile;
                        var attachmentName = await _attachmentRepository.GetAsync(attachment.AttachmentId!);
                        var folderName = Path.Combine("Resources", attachmentName.Title!);
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        var fileName = applicantEntity.Id.ToString();
                        if (!string.IsNullOrEmpty(file))
                        {
                            var isSaved = await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);
                            if (isSaved == true)
                            {
                                serviceResponse.Errors?.Add("Couldn't save applicant attachment.");
                            }
                        }
                    }
                    serviceResponse.Message = "The applicant is successfully edited.";
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Message = "Couldn't edit the requested applicant.";
                    serviceResponse.Success = false;
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }
        if (exceptions.Count > 0)
        {
            throw new AggregateException("One or more exceptions occurred while updating the applicant.", exceptions);
        }

        return serviceResponse;
    }
}
