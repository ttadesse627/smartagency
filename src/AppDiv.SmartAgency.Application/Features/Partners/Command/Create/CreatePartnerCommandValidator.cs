using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Partners.Command.Create
{
    public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
    {
        private readonly IPartnerRepository _repo;
        public CreatePartnerCommandValidator(IPartnerRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.partner.PartnerType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.partner.PartnerName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must has a value")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");
            RuleFor(p => p.partner.ContactPerson)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must has a value")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");
            RuleFor(p => p.partner.Address.CountryId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} must has a value");
            RuleFor(p => p.partner.Address.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull().WithMessage("{PropertyName} must has a value");


            //     RuleForEach(x => x.OrderLines)
            // .ChildRules(orderLine => {
            //     orderLine.RuleFor(x => x.Quantity)
            //         .GreaterThan(0)
            //         .WithMessage("Quantity must be greater than 0.");
            // });

        }
    }
}