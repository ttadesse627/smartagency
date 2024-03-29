﻿using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
{
    private readonly IAttachmentRepository _repo;
    public CreateAttachmentCommandValidator(IAttachmentRepository repo)
    {
        _repo = repo;
        RuleFor(p => p.attachment.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}