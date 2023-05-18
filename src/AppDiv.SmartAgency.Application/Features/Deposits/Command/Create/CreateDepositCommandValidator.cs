using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Deposits
{
    public class CreateDepositCommandValidator: AbstractValidator<CreateDepositCommand>
    {
        private readonly IDepositRepository _repo;
        public CreateDepositCommandValidator(IDepositRepository repo)
        {
            _repo = repo;
            RuleFor(d =>d.deposit.PassportNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}