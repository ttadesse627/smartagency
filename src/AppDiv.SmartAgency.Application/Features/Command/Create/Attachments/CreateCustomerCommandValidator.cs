ausing AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Attachments
{
    public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
    {
        private readonly IAttachmentRepository _repo;
        public CreateAttachmentCommandValidator(IAttachmentRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.attachment.Code)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}