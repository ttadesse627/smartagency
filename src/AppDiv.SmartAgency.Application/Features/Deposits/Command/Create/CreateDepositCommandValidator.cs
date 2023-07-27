using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Deposits.Command.Create
{
    public class CreateDepositCommandValidator : AbstractValidator<CreateDepositCommand>
    {
        private readonly IDepositRepository _repo;
        public CreateDepositCommandValidator(IDepositRepository repo)
        {
            _repo = repo;
            RuleFor(d => d.deposit.PassportNumber)
                .NotEmpty().WithMessage("Passport must not be empty")
                .NotNull().WithMessage("Passport must must has a value")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsPassportExisted).WithMessage("the passport doesn't exist");
            RuleFor(d => d.deposit.DepositAmount)
                .NotNull().WithMessage("Passport must must has a value /required");
            RuleFor(d => d.deposit.Month)
                .NotNull().WithMessage("Month must must has a value /required");

        }

        private async Task<bool> IsPassportExisted(string passportNumber, CancellationToken token)
        {
            return await _repo.GetApplicantPassportByPassportNumber(passportNumber) > 0 ? true : false;

        }
    }
}