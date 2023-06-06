using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Auth.ChangePassword
{
    internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.NewPassword)
             .NotEmpty()
             .NotNull();
            RuleFor(u => u.OldPassword)
             .NotEmpty()
             .NotNull();
            RuleFor(u => u.NewPassword)
              .NotNull()
              .NotEmpty()
              .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$").WithMessage("password must be atleast 6 digit long and must contain atlist one number one number and 1 special character")
              ;

        }
    }
}
