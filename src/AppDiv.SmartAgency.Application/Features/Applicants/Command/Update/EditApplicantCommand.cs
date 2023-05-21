
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.EditApplicantRequests;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public record EditApplicantCommand(EditApplicantRequest request) : IRequest<ServiceResponse<Int32>>
{ }
public class EditApplicantCommandHandler : IRequestHandler<EditApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILookUpRepository _lookUpRepository;
    public EditApplicantCommandHandler(IApplicantRepository applicantRepository, ILookUpRepository lookUpRepository)
    {
        _applicantRepository = applicantRepository;
        _lookUpRepository = lookUpRepository;
    }

    public async Task<ServiceResponse<Int32>> Handle(EditApplicantCommand command, CancellationToken cancellationToken)
    {
        var serviceResponse = new ServiceResponse<Int32>();
        var exceptions = new List<Exception>();
        var applicantEntity = await _applicantRepository.GetApplicantByIdWithAsync(command.request.Id);

        if (applicantEntity != null)
        {
            var editedApplicant = CustomMapper.Mapper.Map<Applicant>(command.request);

            ICollection<LookUp> qualificationTypesLookup = new List<LookUp>();
            ICollection<LookUp> levelOfQualificationsLookup = new List<LookUp>();
            ICollection<LookUp> awardsLookup = new List<LookUp>();
            ICollection<LookUp> skillsLookup = new List<LookUp>();

            foreach (var qualificationTypeId in command.request.Education.QualificationTypes!)
            {
                var qt = await _lookUpRepository.GetAsync(qualificationTypeId);
                qualificationTypesLookup.Add(qt);
            }
            foreach (var levelOfQualificationId in command.request.Education.LevelofQualifications)
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

            // Check if already upto date

            //     if (qualificationTypesLookup.Count > 0)
            //     {
            //         var qts = new List<QualificationType>();
            //         foreach (var qt in qualificationTypesLookup)
            //         {
            //             var qualt = new QualificationType();
            //             qualt.LookUp = qt;

            //             var existingQT = applicantEntity.Education?.QualificationTypes?.FirstOrDefault(q => q.Id == qt.Id);
            //             if (existingQT == null)
            //             {
            //                 qts.Add(qualt);
            //             }
            //             else exceptions.Add(new Exception($"The Applicant is already upto date with an id {qt.Id}"));


            //         }
            //         applicantEntity.Education.QualificationTypes = qts;
            //     }

            //     if (levelOfQualificationsLookup.Count > 0)
            //     {
            //         var loqs = new List<LevelOfQualification>();
            //         foreach (var loq in levelOfQualificationsLookup)
            //         {
            //             var loqual = new LevelOfQualification();
            //             loqual.LookUp = loq;

            //             var existingQT = applicantEntity.Education?.LevelOfQualifications?.FirstOrDefault(q => q.Id == loq.Id);
            //             if (existingQT == null)
            //             {
            //                 loqs.Add(loqual);
            //             }
            //             else exceptions.Add(new Exception($"The Applicant is already upto date with an id {loq.Id}"));


            //         }
            //         applicantEntity.Education.LevelOfQualifications = loqs;
            //     }

            //     if (awardsLookup.Count > 0)
            //     {
            //         var awds = new List<Award>();
            //         foreach (var aw in awardsLookup)
            //         {
            //             var awd = new Award();
            //             awd.LookUp = aw;

            //             var existingAW = applicantEntity.Education?.Awards?.FirstOrDefault(q => q.Id == aw.Id);
            //             if (existingAW == null)
            //             {
            //                 awds.Add(awd);
            //             }
            //             else exceptions.Add(new Exception($"The Applicant is already upto date with an id {aw.Id}"));
            //         }
            //         applicantEntity.Education.Awards = awds;
            //     }

            //     if (skillsLookup.Count > 0)
            //     {
            //         var skls = new List<Skill>();
            //         foreach (var sk in skillsLookup)
            //         {
            //             var skl = new Skill();
            //             skl.LookUp = sk;

            //             var existingSK = applicantEntity.Skills?.FirstOrDefault(q => q.Id == sk.Id);
            //             if (existingSK == null)
            //             {
            //                 skls.Add(skl);
            //             }
            //             else exceptions.Add(new Exception($"The Applicant is already upto date with an id {sk.Id}"));
            //         }
            //         applicantEntity.Skills = skls;
            //     }


            //     serviceResponse.Success = _applicantRepository.SaveChanges();
            //     // serviceResponse.Data = await _applicantRepository.EditApplicantAsync(editedApplicant);



            // else exceptions.Add(new Exception($"The applicant with an Id {command.request.Id} does not exist."));



            // // Update the retrieved entities with the request


            // // _applicantRepository.Update(editedApplicant);

            // if (serviceResponse.Success)
            // {
            //     serviceResponse.Message = "Successfully updated the applicant.";
            // }
            // if (exceptions.Count > 0)
            // {
            //     foreach (var exc in exceptions)
            //     {
            //         throw exc;
            //     }
            // }

            // Update the QualificationTypes
            if (qualificationTypesLookup.Count > 0 && applicantEntity.Education != null)
            {
                foreach (var qtLookup in qualificationTypesLookup)
                {
                    // Check if the QualificationType already exists
                    var existingQT = applicantEntity.Education.QualificationTypes
                        .FirstOrDefault(qt => qt.LookUp.Id == qtLookup.Id);

                    if (existingQT == null)
                    {
                        // Create a new QualificationType and add it to the list in the parent entity
                        var newQT = new QualificationType
                        {
                            LookUp = qtLookup
                        };
                        applicantEntity.Education.QualificationTypes.Add(newQT);
                    }
                    else
                    {
                        exceptions.Add(new Exception($"The Applicant is already up-to-date with a QualificationType with Id {qtLookup.Id}"));
                    }
                }
            }

            // Update the LevelOfQualifications
            if (levelOfQualificationsLookup.Count > 0 && applicantEntity.Education != null)
            {
                foreach (var loqLookup in levelOfQualificationsLookup)
                {
                    // Check if the LevelOfQualification already exists
                    var existingLOQ = applicantEntity.Education?.LevelOfQualifications?
                        .FirstOrDefault(loq => loq.LookUp.Id == loqLookup.Id);

                    if (existingLOQ == null)
                    {
                        // Create a newLevelOfQualification and add it to the list in the parent entity
                        var newLOQ = new LevelOfQualification
                        {
                            LookUp = loqLookup
                        };
                        applicantEntity.Education.LevelOfQualifications.Add(newLOQ);
                    }
                    else
                    {
                        exceptions.Add(new Exception($"The Applicant is already up-to-date with a LevelOfQualification with Id {loqLookup.Id}"));
                    }
                }
            }

            // Update the Awards
            if (awardsLookup.Count > 0 && applicantEntity.Education != null)
            {
                foreach (var awardLookup in awardsLookup)
                {
                    // Check if the Award already exists
                    var existingAward = applicantEntity.Education?.Awards?
                        .FirstOrDefault(a => a.LookUp.Id == awardLookup.Id);

                    if (existingAward == null)
                    {
                        // Create a new Award and add it to the list in the parent entity
                        var newAward = new Award
                        {
                            LookUp = awardLookup
                        };
                        applicantEntity.Education.Awards.Add(newAward);
                    }
                    else
                    {
                        exceptions.Add(new Exception($"The Applicant is already up-to-date with an Award with Id {awardLookup.Id}"));
                    }
                }
            }

            // Update the Skills
            if (skillsLookup.Count > 0 && applicantEntity != null)
            {
                foreach (var skillLookup in skillsLookup)
                {
                    // Check if the Skill already exists
                    var existingSkill = applicantEntity.Skills?
                        .FirstOrDefault(s => s.LookUp.Id == skillLookup.Id);

                    if (existingSkill == null)
                    {
                        // Create a new Skill and add it to the list in the parent entity
                        var newSkill = new Skill
                        {
                            LookUp = skillLookup
                        };
                        applicantEntity.Skills?.Add(newSkill);
                    }
                    else
                    {
                        exceptions.Add(new Exception($"The Applicant is already up-to-date with a Skill with Id {skillLookup.Id}"));
                    }
                }
            }

            // Save changes to the database
            try
            {
                serviceResponse.Data = await _applicantRepository.EditApplicantAsync(editedApplicant);
                // serviceResponse.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);

                if (serviceResponse.Data >= 1)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Successfully updated the applicant.";
                }
            }
            catch (Exception ex)
            {
                // Handle any database errors and add them to the exceptions list
                exceptions.Add(ex);
            }
        }

        // Handle any exceptions
        if (exceptions.Count > 0)
        {
            throw new AggregateException("One or more exceptions occurred while updating the applicant.", exceptions);
        }

        return serviceResponse;
    }
}