using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.LookUps
{
     public class CreateLookUpCommandValidator : AbstractValidator<CreateLookUpCommand>
    {
        private readonly ILookUpRepository _repo;
        public CreateLookUpCommandValidator(ILookUpRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.lookUp.Value)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}