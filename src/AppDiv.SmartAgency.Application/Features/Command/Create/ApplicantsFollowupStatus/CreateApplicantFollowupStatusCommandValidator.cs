using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.ApplicantsFollowupStatus
{
    public class CreateApplicantFollowupStatusCommandValidator: AbstractValidator<CreateApplicantFollowupStatusCommand>
    {
        private readonly IApplicantFollowupStatusRepository _repo;
        public CreateApplicantFollowupStatusCommandValidator(IApplicantFollowupStatusRepository repo)
        {
            _repo = repo;
            RuleFor(afs =>afs.applicantFollowupStatus.PassportNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}