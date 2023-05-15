using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Pagess
{
    public class CreatePageCommandValidator :AbstractValidator<CreatePageCommand>
    {
        private readonly IPageRepository _repo;
        public CreatePageCommandValidator(IPageRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.page.Link)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.page.Title)
                 .NotEmpty().WithMessage("{PropertyName} is required.")
                 .NotNull()
                 .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");    
        }
    }
}