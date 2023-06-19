using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.DeletedInfos.Query;

public record GetDeletedApplicantQuery(Guid Id) : IRequest<GetApplicantResponseDTO> { }
public class GetDeletedApplicantQueryHandler : IRequestHandler<GetDeletedApplicantQuery, GetApplicantResponseDTO>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetDeletedApplicantQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task<GetApplicantResponseDTO> Handle(GetDeletedApplicantQuery request, CancellationToken cancellationToken)
    {
        var eagerLoadedProperties = new string[]
                                    {
                                        "IssuingCountry", "PassportIssuedPlace","MaritalStatus",
                                        "Health","Religion","Jobtitle","BrokerName","Branch",
                                        "Experience","Language","Salary","DesiredCountry",
                                        "LanguageSkills.Language","Skills.LookUp","Experiences.Country",
                                        "Education.QualificationTypes.LookUp","Education.LevelOfQualifications.LookUp",
                                        "Education.Awards.LookUp","BankAccount","EmergencyContact.Relationship",
                                        "EmergencyContact.Address.Region","Witnesses","Beneficiaries.Relationship",
                                        "AttachmentFiles.Attachment","Address.Region"
                                    };
        var applicantEntity = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == request.Id && appl.IsDeleted == true, eagerLoadedProperties);
        var applicantResponse = CustomMapper.Mapper.Map<GetApplicantResponseDTO>(applicantEntity);
        return applicantResponse;
    }
}
