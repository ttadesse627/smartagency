using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Resources.Command.Create
{
    public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
    {
        private readonly IResourceRepository _repo;
        public CreateResourceCommandValidator(IResourceRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.ResourceName.Trim())
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }
    }
}