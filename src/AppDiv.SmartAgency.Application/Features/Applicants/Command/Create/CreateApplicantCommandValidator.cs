using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Create
{
    public class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
    {
        private readonly IApplicantRepository _repo;
        public CreateApplicantCommandValidator(IApplicantRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.ApplicantRequest.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            //RuleFor(e => e)
            //   .MustAsync(phoneNumberUnique)
            //   .WithMessage("A Customer phoneNumber already exists.");
        }

        // private async Task<bool> phoneNumberUnique(CreateApplicantCommand request, CancellationToken token)
        // {
        //     var member = await _repo.GetByIdAsync(request.ApplicantRequest.Address.PhoneNumber);
        //     if (member == null)
        //         return true;
        //     else return false;
        // }

    }
}