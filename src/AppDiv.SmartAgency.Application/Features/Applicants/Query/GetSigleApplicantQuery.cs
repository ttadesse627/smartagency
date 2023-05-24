

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public record GetSingleApplicantQuery(Guid id) : IRequest<GetApplicantResponseDTO>
{}
public class GetSingleApplicantQueryHandler : IRequestHandler<GetSingleApplicantQuery, GetApplicantResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IApplicantRepository _applicantRepository;
    public GetSingleApplicantQueryHandler(IApplicantRepository applicantRepository, IMediator mediator)
    {
        _applicantRepository = applicantRepository;
        _mediator = mediator;
    }
    public async Task<GetApplicantResponseDTO> Handle(GetSingleApplicantQuery request, CancellationToken cancellationToken)
    {
        var eagerLoadedProperties = new string[]
                                    {
                                        "IssuingCountry", "PassportIssuedPlace","MaritalStatus",
                                        "Health","Religion","Jobtitle","BrokerName","Branch",
                                        "Experience","Language","Salary","DesiredCountry",
                                        "LanguageSkills.Language","Skills.LookUp","Experiences.Country",
                                        "Education.QualificationTypes.LookUp","Education.LevelOfQualifications.LookUp",
                                        "Education.Awards.LookUp","BankAccount","EmergencyContact.Relationship",
                                        "EmergencyContact.Address.AddressRegion","Representative.Address.AddressRegion",
                                        "Witnesses","Beneficiaries.Relationship","AttachmentFiles.Attachment",
                                        "Address.AddressRegion"
                                    };
        var applicantEntity = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == request.id && appl.IsDeleted == false, eagerLoadedProperties);
        var applicantResponse = CustomMapper.Mapper.Map<GetApplicantResponseDTO>(applicantEntity);
        return applicantResponse;
    }
}
