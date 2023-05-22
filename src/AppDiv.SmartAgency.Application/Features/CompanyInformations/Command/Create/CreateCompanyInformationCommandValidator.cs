using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Create
{
    public class CreateCompanyInformationCommandValidator: AbstractValidator<CreateCompanyInformationCommand>
    {
        private readonly ICompanyInformationRepository _repo;
        public CreateCompanyInformationCommandValidator(ICompanyInformationRepository repo)
        {
            _repo = repo;
            RuleFor(ci => ci.companyInformation.CompanyName )
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}