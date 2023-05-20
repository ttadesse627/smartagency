
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
        var editedApplicant = await _applicantRepository.GetAsync(command.request.Id);
        editedApplicant = CustomMapper.Mapper.Map<Applicant>(command.request);

        ICollection<LookUp> qualificationTypes = new List<LookUp>();
        ICollection<LookUp> levelOfQualifications = new List<LookUp>();
        ICollection<LookUp> awards = new List<LookUp>();
        ICollection<LookUp> skills = new List<LookUp>();

        foreach (var qualificationType in command.request.Education.QualificationTypes!)
        {
            var qt = await _lookUpRepository.GetAsync(qualificationType);
            qualificationTypes.Add(qt);
        }
        foreach (var levelOfQualification in command.request.Education.LevelofQualifications)
        {
            var loq = await _lookUpRepository.GetAsync(levelOfQualification);
            levelOfQualifications.Add(loq);
        }
        foreach (var award in command.request.Education.Awards!)
        {
            var awd = await _lookUpRepository.GetAsync(award);
            awards.Add(awd);
        }
        foreach (var skill in command.request.Skills!)
        {
            var sk = await _lookUpRepository.GetAsync(skill);
            skills.Add(sk);
        }



        // Update the retrieved entities with the request

        // editedApplicant.Education!.QualificationTypes = qualificationTypes;
        // editedApplicant.Education.LevelOfQualifications = levelOfQualifications;
        // editedApplicant.Education.Awards = awards;
        // editedApplicant.Skills = skills;

        serviceResponse.Data = await _applicantRepository.EditApplicantAsync(editedApplicant);
        // _applicantRepository.Update(editedApplicant);
        if (serviceResponse.Success)
        {
            serviceResponse.Message = "Successfully updated the applicant.";
        }
        return serviceResponse;
    }
}