using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Auth.ForgotPassword
{
    internal class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(u => u.UserName)
             .NotEmpty()
             .NotNull();
            RuleFor(u => u.ClientURI)
             .NotEmpty()
             .NotNull();

        }
    }
}
