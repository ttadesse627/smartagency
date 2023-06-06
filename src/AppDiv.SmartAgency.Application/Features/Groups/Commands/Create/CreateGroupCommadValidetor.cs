

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Groups.Commands.Create
{
    public class CreateGroupComadValidetor : AbstractValidator<CreateGroupCommand>
    {
        private readonly IGroupRepository _repo;
        public CreateGroupComadValidetor(IGroupRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.group.GroupName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(g => g.group.GroupName)
              .MustAsync(groupNameUnique)
              .WithMessage("A Group Name already exists.");
        }

        private async Task<bool> groupNameUnique(string groupName, CancellationToken token)
        {
            var member = await _repo.GetAsync(groupName);
            if (member == null)
                return true;
            else return false;
        }

    }
}