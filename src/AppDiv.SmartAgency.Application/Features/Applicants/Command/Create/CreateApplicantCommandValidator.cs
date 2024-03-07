using System.Data;
using System.ComponentModel.Design;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Create
{
    public class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
    {
        private readonly IApplicantRepository _repo;


        public CreateApplicantCommandValidator(IApplicantRepository repo)
        {
            _repo = repo;

            RuleFor(app => app.ApplicantRequest.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(app => app.ApplicantRequest.MiddleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(app => app.ApplicantRequest.LastName)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(app => app.ApplicantRequest.PassportNumber)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value")
                 .MustAsync((passportNumber, token) => PassportIsUnique(passportNumber, token)).WithMessage("Passport Already Exists!");

            RuleFor(app => app.ApplicantRequest.BirthDate)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value")
                 .Must(birthDate => !IsUnderAge(birthDate)).WithMessage("Applicant should be above 18 years old!");

            RuleFor(app => app.ApplicantRequest.IssuingCountryId)
                 .NotEmpty().WithMessage("Issuing Country must not be empty")
                 .NotNull().WithMessage("Issuing Country must has a value");

            RuleFor(app => app.ApplicantRequest.PassportIssuedPlaceId)
                 .NotEmpty().WithMessage("Issued Place must not be empty")
                 .NotNull().WithMessage("Issued Place must has a value");

            RuleFor(app => app.ApplicantRequest.IssuedDate)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value");

            RuleFor(app => app.ApplicantRequest.PassportExpiryDate)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value")
                 .Must((app, passportExpiryDate) => IsPassportExpiryDateValid(app.ApplicantRequest.IssuedDate, passportExpiryDate))
                     .WithMessage("Passport expiry date must be greater than passport issued date");
            RuleFor(app => app.ApplicantRequest.PlaceOfBirth)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value");

            RuleFor(app => app.ApplicantRequest.IssuedDate)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value");
            RuleFor(app => app.ApplicantRequest.AmharicFullName)
                             .NotEmpty().WithMessage("{PropertyName} must not be empty")
                             .NotNull().WithMessage("{PropertyName} must has a value");

            RuleFor(app => app.ApplicantRequest.MaritalStatusId)
                 .NotEmpty().WithMessage("Marital Status must not be empty")
                 .NotNull().WithMessage("Marital Status must has a value");

            RuleFor(app => app.ApplicantRequest.Gender)
                 .NotEmpty().WithMessage("{PropertyName} must not be empty")
                 .NotNull().WithMessage("{PropertyName} must has a value");

            RuleFor(app => app.ApplicantRequest.HealthId)
                 .NotEmpty().WithMessage("Health must not be empty")
                 .NotNull().WithMessage("Health  must has a value");

            RuleFor(app => app.ApplicantRequest.ReligionId)
                 .NotEmpty().WithMessage("Religion must not be empty")
                 .NotNull().WithMessage("Religion must has a value");
            RuleFor(app => app.ApplicantRequest.JobtitleId)
                 .NotEmpty().WithMessage("Jobtitle must not be empty")
                 .NotNull().WithMessage("Jobtitle must has a value");

            RuleFor(app => app.ApplicantRequest.ExperienceId)
                .NotEmpty().WithMessage("Experience must not be empty")
                .NotNull().WithMessage("Experience must has a value");

            RuleFor(app => app.ApplicantRequest.LanguageId)
                .NotEmpty().WithMessage("Language must not be empty")
                .NotNull().WithMessage("Language must has a value!");

            RuleFor(app => app.ApplicantRequest.SalaryId)
                .NotEmpty().WithMessage("Salary must not be empty")
                .NotNull().WithMessage("Salary must has a value!");

            RuleFor(app => app.ApplicantRequest.DesiredCountryId)
                .NotEmpty().WithMessage("Desired country must not be empty!")
                .NotNull().WithMessage("Desired country must has a value!");

            RuleFor(app => app.ApplicantRequest.Address)
                .NotNull().WithMessage("Applicant address must have a value");
            RuleFor(app => app.ApplicantRequest.Address!.RegionId)
                .NotNull().WithMessage("Applicant Region/Country must have a value")
                .When(app => app.ApplicantRequest.Address != null);
            RuleFor(app => app.ApplicantRequest.Address!.Adress)
                .NotNull().WithMessage("Applicant address 'address' must have a value")
                .NotEmpty().WithMessage("Applicant address 'address' must not be an empty string")
                .When(app => app.ApplicantRequest.Address != null);

            RuleFor(app => app.ApplicantRequest.Skill)
               .NotNull().WithMessage("Skill  must has a value!")
               .NotEmpty().WithMessage("Skill must not be empty");
            RuleFor(app => app.ApplicantRequest.Skill!.LanguageSkills)
               .NotNull().WithMessage("Language Skills  must have value!")
               .NotEmpty().WithMessage("language must not be empty")
               .When(app => app.ApplicantRequest.Skill != null);
            RuleFor(app => app.ApplicantRequest.Skill!.Skills)
               .NotNull().WithMessage("Technical Skills must have a value!")
               .NotEmpty().WithMessage("Technical Skills must not be empty!")
               .When(app => app.ApplicantRequest.Skill != null);

        }

        private async Task<bool> PassportIsUnique(string passportNumber, CancellationToken token)
        {
            var member = await _repo.GetApplicantByPassportNumber(passportNumber);
            if (member == null)
                return true;
            else return false;
        }
        private bool IsUnderAge(DateTime birthDate)
        {
            return DateTime.Now.Year - birthDate.Year < 18 ? false : true;

        }

        private bool IsPassportExpiryDateValid(DateTime passportIssuedDate, DateTime passportExpiryDate)
        {
            return passportExpiryDate > passportIssuedDate;
        }
    }
}