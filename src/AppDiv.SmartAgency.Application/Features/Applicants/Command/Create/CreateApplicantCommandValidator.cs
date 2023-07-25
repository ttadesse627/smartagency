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
            /*    
                      RuleFor(app => app.ApplicantRequest.Skill)
                         .NotNull().WithMessage("Skill  must has a value!")
                          .DependentRules(a)

                      RuleFor(app => app.ApplicantRequest.Skill!.LanguageSkills)
                         .NotNull().WithMessage("Language Skills  must have value!");
                      RuleFor(app => app.ApplicantRequest.Skill!.Skills)
                         .NotNull().WithMessage("Technical Skills must have a value!");
                      RuleFor(app => app.ApplicantRequest.Education)
                         .NotNull().WithMessage("education  must has a value!")
                         .NotEmpty().WithMessage("education must not be empty");
                      RuleFor(app => app.ApplicantRequest.Education.QualificationTypes)
                        .NotNull().WithMessage("Qualification types  must has a value!")
                        .NotEmpty().WithMessage("Qualification types  must not be empty");
                      RuleFor(app => app.ApplicantRequest.Education.LevelofQualifications)
                        .NotNull().WithMessage("level of qualifications  must has a value!")
                        .NotEmpty().WithMessage("level of qualifications  must not be empty");
                      RuleFor(app => app.ApplicantRequest.Education.QualificationTypes)
                         .NotNull().WithMessage("Qualification types  must has a value!")
                         .NotEmpty().WithMessage("Qualification types  must not be empty");

                      RuleFor(app => app.ApplicantRequest.EmergencyContact)
                           .Must(contact => contact != null).WithMessage("Emergency Contact must have a value!");

                      RuleFor(app => app.ApplicantRequest.EmergencyContact)
                          .NotNull().When(app => app.ApplicantRequest.EmergencyContact != null)
                          .DependentRules(() =>
                          {
                              RuleFor(app => app.ApplicantRequest.EmergencyContact.NameOfContactPerson)
                                  .NotNull().WithMessage("NameOfContactPerson must have a value!")
                                  .NotEmpty().WithMessage("NameOfContactPerson must not be empty");

                              RuleFor(app => app.ApplicantRequest.EmergencyContact.RelationshipId)
                                  .NotNull().WithMessage("Relationship must have a value!")
                                  .NotEmpty().WithMessage("Relationship must not be empty");
                          })
                          .When(app => app.ApplicantRequest.EmergencyContact != null, ApplyConditionTo.CurrentValidator);
                      RuleFor(app => app.ApplicantRequest.EmergencyContact.Address)
                         .NotNull().WithMessage("Contact Person Address  must has a value!");
                      RuleFor(app => app.ApplicantRequest.EmergencyContact.Address.RegionId)
                         .NotNull().WithMessage("Contact person Region/Country  must has a value!")
                         .NotEmpty().WithMessage("Contact person Region/Country must not be empty");
                      RuleFor(app => app.ApplicantRequest.EmergencyContact.Address.Adress)
                         .NotNull().WithMessage("Contact Person Adress  must has a value!")
                         .NotEmpty().WithMessage("Contact Person Address address must not be empty");
                      RuleFor(app => app.ApplicantRequest.Witness)
                         .NotNull().WithMessage(" Witness must has a value!");
                      RuleFor(app => app.ApplicantRequest.Witness.Representative)
                         .NotNull().WithMessage("Representative   must has a value!")
                         .NotEmpty().WithMessage("Represenatative  must not be empty");
                      RuleFor(app => app.ApplicantRequest.Witness.Representative.FullName)
                        .NotNull().WithMessage("Representative Fullname must has a value!")
                        .NotEmpty().WithMessage("Represenatative Fullname must not be empty");
                      RuleFor(app => app.ApplicantRequest.Witness.Witnesses)
                         .NotNull().WithMessage("Witness  must has a value!")
                         .NotEmpty().WithMessage("Witness  must not be empty")
                         .ForEach(witness =>
                               witness.NotEmpty().WithMessage("witness must not be empty")
                                      .NotNull().WithMessage("witness must has a value")
                                      .ChildRules(w =>
                                      {
                                          w.RuleFor(witness => witness.FullName)
                                               .NotEmpty().WithMessage(" witnesse's fullname must not be empty")
                                               .NotNull().WithMessage("Witnesse's fullname must has a value!");
                                          w.RuleFor(witness => witness.Address)
                                                .NotEmpty().WithMessage("witnesse's Address must not be empty")
                                                .NotNull().WithMessage("Witnesse's Address must has a value");
                                          w.RuleFor(witness => witness.PhoneNumber)
                                                .NotEmpty().WithMessage("witnesse's Phonenumber must not be empty")
                                                .NotNull().WithMessage("Witnesse's phonenumber must has a value");
                                      })
                               );
                      RuleFor(app => app.ApplicantRequest.Address)
                          .NotNull().WithMessage("Applicant address must have a value")
                          .NotEmpty().WithMessage("Applicant address must not be empty");
                      RuleFor(app => app.ApplicantRequest.Address.RegionId)
                           .NotNull().WithMessage("Applicant Region/Country must have a value")
                           .NotEmpty().WithMessage("Applicant Region/Country  must not be empty");
                      RuleFor(app => app.ApplicantRequest.Address.Adress)
                           .NotNull().WithMessage("Applicant address 'address' must have a value")
                           .NotEmpty().WithMessage("Applicant address '' must not be string");
          */













            // RuleFor(app => app.ApplicantRequest.PassportNumber)
            //           .Must(passportNumber => _helper.IsUnique("Applicants", "PassportNumber", passportNumber))
            //           .WithMessage("Passport number must be unique.");

            //RuleFor(e => e)
            //   .MustAsync(phoneNumberUnique)
            //   .WithMessage("A Customer phoneNumber already exists.");
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


        //private async Task<bool> phoneNumberUnique(CreateCustomerCommand request, CancellationToken token)
        //{
        //    var member = await _repo.GetByIdAsync(request.FirstName);
        //    if (member == null)
        //        return true;
        //    else return false;
        //}


    }
}