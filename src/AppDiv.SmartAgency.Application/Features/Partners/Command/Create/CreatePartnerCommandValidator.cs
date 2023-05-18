using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Partners
{
    public class CreatePartnerCommandValidator: AbstractValidator<CreatePartnerCommand>
    {
        private readonly IPartnerRepository _repo;
        public CreatePartnerCommandValidator(IPartnerRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.partner.PartnerName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}