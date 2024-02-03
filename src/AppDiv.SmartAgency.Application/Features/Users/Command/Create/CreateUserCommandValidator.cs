using AppDiv.SmartAgency.Application.Interfaces;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IIdentityService _repo;
        public CreateUserCommandValidator(IIdentityService repo)
        {
            _repo = repo;
            RuleFor(u => u.Request.UserName)
            .NotNull()
            .NotEmpty()
            .MustAsync(BeUniqueUsername).WithMessage("userName already exists ")
            .Matches("^[a-zA-Z0-9-._@+]+$").WithMessage("invalid user name:\n user name cannot have spaces or special characters except -._@+");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            var isUnique = false;
            var user = await _repo.GetByUsernameAsync(username);
            if (user == null) isUnique = true;
            return isUnique;
        }


    }
}