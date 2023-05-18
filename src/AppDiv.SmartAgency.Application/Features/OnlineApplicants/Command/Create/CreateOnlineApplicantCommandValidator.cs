using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Command.Create
{
    public class CreateOnlineApplicantCommandValidator: AbstractValidator<CreateOnlineApplicantCommand>
    {
        private readonly IOnlineApplicantRepository _repo;
        public CreateOnlineApplicantCommandValidator(IOnlineApplicantRepository repo)
        {
            _repo = repo;
            RuleFor(oa => oa.onlineApplicant.Passport)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}