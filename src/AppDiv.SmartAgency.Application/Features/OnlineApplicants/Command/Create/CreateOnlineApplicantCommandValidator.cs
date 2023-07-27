using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Create
{
    public class CreateOnlineApplicantCommandValidator : AbstractValidator<CreateOnlineApplicantCommand>
    {
        private readonly IOnlineApplicantRepository _repo;
        public CreateOnlineApplicantCommandValidator(IOnlineApplicantRepository repo)
        {
            _repo = repo;
            RuleFor(oa => oa.onlineApplicant.Passport)
                .NotEmpty().WithMessage("Passport is must not be empty.")
                .NotNull().WithMessage("Passport must have a value")
                .MaximumLength(50).WithMessage("Passport must not exceed 50 characters.")
                .MustAsync(PassportIsUnique).WithMessage("Applicant with this passport number aleardy Registered");
            RuleFor(oa => oa.onlineApplicant.FullName)
                .NotEmpty().WithMessage("Fullname must not be empty")
                .NotNull().WithMessage("Fullname must has a value");
            RuleFor(oa => oa.onlineApplicant.Sex)
                .NotEmpty().WithMessage("Sex must not be empty")
                .NotNull().WithMessage("Sex must has a value");
            RuleFor(oa => oa.onlineApplicant.MaritalStatusId)
                .NotEmpty().WithMessage("Marital Status must not be empty")
                .NotNull().WithMessage("Marital Status must has a value");
            RuleFor(oa => oa.onlineApplicant.ExperienceId)
                .NotEmpty().WithMessage("Experience must not be empty")
                .NotNull().WithMessage("Experience must has a value");
            RuleFor(oa => oa.onlineApplicant.DesiredCountryId)
                .NotEmpty().WithMessage("Desired Country must not be empty")
                .NotNull().WithMessage("Desired Country must has a value");


        }
        private async Task<bool> PassportIsUnique(string passportNumber, CancellationToken token)
        {
            var isPassportExisted = await _repo.GetOnlineApplicantByPassportNumber(passportNumber);
            if (isPassportExisted > 0)
            {
                return false;
            }
            return true;

        }
    }
}