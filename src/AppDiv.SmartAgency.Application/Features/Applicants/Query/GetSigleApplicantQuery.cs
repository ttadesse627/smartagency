

using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query;
public record GetSingleApplicantQuery(Guid id) : IRequest<GetApplicantResponseDTO>
{ }
public class GetSingleApplicantQueryHandler : IRequestHandler<GetSingleApplicantQuery, GetApplicantResponseDTO>
{
    private readonly IMediator _mediator;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IFileService _fileService;
    public GetSingleApplicantQueryHandler(IApplicantRepository applicantRepository, IMediator mediator, IFileService fileService)
    {
        _applicantRepository = applicantRepository;
        _mediator = mediator;
        _fileService = fileService;
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
                                        "Education.Awards.LookUp","BankAccount","EmergencyContact.Relationship", "Attachments",
                                        "EmergencyContact.Address.Region","Witnesses","Beneficiaries.Relationship","Address.Region"
                                    };
        var applicantEntity = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == request.id && appl.IsDeleted == false, eagerLoadedProperties);
        var attachmentFiles = applicantEntity.Attachments;
        var fileTypes = new List<string>();
        if (attachmentFiles != null && attachmentFiles.Count > 0)
        {
            foreach (var attachmentFile in attachmentFiles)
            {
                fileTypes.Add(attachmentFile.Title!);
            }
        }
        var applicantResponse = CustomMapper.Mapper.Map<GetApplicantResponseDTO>(applicantEntity);

            if (DateTime.TryParse(applicantResponse.BirthDate, out DateTime birthDate))
                        {
                            applicantResponse.BirthDate = birthDate.ToString("yyyy-MM-dd");
                        }
                applicantResponse.Gender= applicantResponse.Gender.ToString();





        var fileResults = _fileService.GetFiles(fileTypes, applicantEntity.Id.ToString());
        var attchFiles = new List<AttachmentFileResponseDTO>();
        if (fileResults.Count > 0)
        {
            foreach (var fileResult in fileResults)
            {
                var fileResponse = new AttachmentFileResponseDTO
                {
                    AttachmentFile = Convert.ToBase64String(fileResult.File),
                    AttachmentType = fileResult.FileName
                };
                attchFiles.Add(fileResponse);
            }
            applicantResponse.AttachmentFiles = attchFiles;
        }
        return applicantResponse;
    }
}
