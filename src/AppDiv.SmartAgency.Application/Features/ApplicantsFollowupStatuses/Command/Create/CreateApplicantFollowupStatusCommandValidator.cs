using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Create
{
    public class CreateApplicantFollowupStatusCommandValidator : AbstractValidator<CreateApplicantFollowupStatusCommand>
    {
        private readonly IApplicantFollowupStatusRepository _repo;
        public CreateApplicantFollowupStatusCommandValidator(IApplicantFollowupStatusRepository repo)
        {
            _repo = repo;
            RuleFor(afs => afs.applicantFollowupStatus.PassportNumber)
                .NotEmpty().WithMessage("Passport is must not be empty.")
                .NotNull().WithMessage("passport must has a value")
                .MaximumLength(50).WithMessage("Passport must not exceed 50 characters.")
                .MustAsync(IsPassportExisted).WithMessage("Invalid passport or passport doesn't exist!");
            RuleFor(afs => afs.applicantFollowupStatus.FollowupStatusId)
                .NotEmpty().WithMessage("Followup status must not be empty")
                .NotNull().WithMessage("Followup status must has a value");
            RuleFor(afs => afs.applicantFollowupStatus.Month)
                .NotEmpty().WithMessage("Month status must not be empty")
                .NotNull().WithMessage("Month status must has a value");
        }

        private async Task<bool> IsPassportExisted(string passportNumber, CancellationToken token)
        {
            return await _repo.GetApplicantByPassportNumber(passportNumber) > 0 ? true : false;
        }
    }
}