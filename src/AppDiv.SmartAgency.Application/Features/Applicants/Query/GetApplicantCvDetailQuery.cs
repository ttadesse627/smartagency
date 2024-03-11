using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Query
{

    public record GetApplicantCvDetailQuery(Guid Id) : IRequest<ApplicantCvResponseDTO> { }

    public class GetApplicantCvDetailHandler : IRequestHandler<GetApplicantCvDetailQuery, ApplicantCvResponseDTO>
    {
        private readonly IApplicantRepository _applicantRepository;


        private readonly IFileService _fileService;

        public GetApplicantCvDetailHandler(IApplicantRepository applicantRepository, IFileService fileService)
        {
            _applicantRepository = applicantRepository;
            _applicantRepository = applicantRepository;
            _fileService = fileService;
        }
        public async Task<ApplicantCvResponseDTO> Handle(GetApplicantCvDetailQuery request, CancellationToken cancellationToken)
        {
            // var response = await _applicantRepository.GetApplicantCvDetail(request.Id);
            // return response;


            var response = new ApplicantCvResponseDTO();
            // var overseasExperiences = new List<OverseasExperienceResponseDTO>();
            // var skills = new List<string>();
            // var languages = new List<LanguagesResponseDTO>();
            var eagerLoadedProperties = new string[]
            {
               "Salary","Religion","Jobtitle","MaritalStatus","Skills.LookUp",
               "Education.LevelOfQualifications.LookUp","Address","EmergencyContact",
               "EmergencyContact.Address","Experiences.Country","PassportIssuedPlace",
               "Attachments","LanguageSkills.Language"
            };

            var appResponse = await _applicantRepository.GetWithPredicateAsync(appl => appl.Id == request.Id, eagerLoadedProperties);
            if (appResponse != null)
            {
                var overViewResponse = new OverviewResponseDTO
                {
                    RefNumber = "001",
                    FullName = $"{appResponse.FirstName} {appResponse.MiddleName} {appResponse.LastName}",
                    Religion = appResponse.Religion?.Value,
                    DesiredPosition = appResponse.Jobtitle?.Value,
                    Salary = appResponse.Salary?.Value,
                    Age = DateTime.Now.Year - appResponse.BirthDate.Year,
                    Sex = appResponse.Gender.ToString()
                };

                var personalInfo = new PersonalInfoResponseDTO
                {
                    Id = appResponse.Id,
                    Nationality = appResponse.CurrentNationality?.Value,
                    DateOfBirth = appResponse.BirthDate.ToString("yyyy-MM-dd"),
                    PlaceOfBirth = appResponse.PlaceOfBirth,
                    MaritalStatus = appResponse.MaritalStatus?.Value,
                    NumberOfChildren = appResponse.NumberOfChildren,
                    Height = appResponse.Height,
                    Weight = appResponse.Weight,
                    EducationQualification = appResponse.Education?.LevelOfQualifications.FirstOrDefault()?.LookUp?.Value,
                    PhoneNumber = appResponse.Address?.PhoneNumber,
                };

                var overseasExperiences = appResponse.Experiences.Select(e => new OverseasExperienceResponseDTO
                {
                    Country = e.Country?.Value,
                    Period = e.PeriodLength,
                    Position = e.Position
                }).ToList();

                var passportInfo = new PassportInfoResponseDTO
                {
                    PassportNumber = appResponse.PassportNumber,
                    IssuedDate = appResponse.IssuedDate.ToString("yy-MM-dd"),
                    ExpiryDate = appResponse.PassportExpiryDate.ToString("yy-MM-dd"),
                    PassportIssuedPlace = appResponse.PassportIssuedPlace?.Value,
                    NextOfKinName = appResponse.EmergencyContact?.NameOfContactPerson,
                    NextOfKinNumber = appResponse.EmergencyContact?.Address?.PhoneNumber
                };

                var skills = appResponse.Skills.Select(s => s.LookUp?.Value).ToList();
                var languages = appResponse.LanguageSkills.Select(l => new LanguagesResponseDTO
                {
                    LanguageName = l.Language?.Value,
                    Proficiency = l.Proficiency.ToString()
                }).ToList();

                var attachmentTypes = appResponse.Attachments.Where(att => att.ShowOnCv == true).Select(att => att.Title).ToList();
                var attachment = new AttachmentsResponseDTO();
                if (appResponse.Attachments != null && appResponse.Attachments.Any())
                {
                    attachment.Photo = Convert.ToBase64String(_fileService.getFile(appResponse.Id.ToString(), "3x4", null).Item1);
                    attachment.FullSizePhoto = Convert.ToBase64String(_fileService.getFile(appResponse.Id.ToString(), "Full Size", null).Item1);
                }
                response.Overview = overViewResponse;
                response.PersonalInfo = personalInfo;
                response.PassportInfo = passportInfo;
                response.OverseasExperiences = overseasExperiences;
                response.Skills = skills!;
                response.Languages = languages;
                response.Attachments = attachment;
                response.AttachmentTypes = attachmentTypes!;
            }

            return response;
        }

    }
}