using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Create
{
    public record CreateUserCommand(AddUserRequest Request) : IRequest<ServiceResponse<int>> { }

    public class CreateUserCommandHandler(IIdentityService identityService, IGroupRepository groupRepository) : IRequestHandler<CreateUserCommand, ServiceResponse<int>>
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<ServiceResponse<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            var request = command.Request;
            if (request.Password != request.ConfirmationPassword)
            {
                var msg = "The password doesn't match";
                response.Errors?.Add(msg);
                throw new BadRequestException("The password doesn't match");
            }

            var validator = new CreateUserCommandValidator(_identityService);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if ((request.PositionId != null || request.BranchId != null) && request.PartnerId != null)
            {
                throw new BadRequestException("The user should belong either only to main agency or partner; but not both.");
            }

            // Check and log validation errors
            if (validationResult.Errors.Count > 0 || !validationResult.IsValid)
            {
                response.Errors = new List<string>();
                foreach (var error in validationResult.Errors) response.Errors.Add(error.ErrorMessage);
                response.Message = response.Errors[0];
            }
            if (validationResult.IsValid)
            {
                var mappedUser = CustomMapper.Mapper.Map<ApplicationUser>(request);

                mappedUser.Email = request.Address?.Email;
                if (request.UserGroups?.Count > 0)
                {
                    var listGroup = await _groupRepository.GetMultipleUserGroups(request.UserGroups);
                    mappedUser.UserGroups = listGroup;
                }

                var createResponse = await _identityService.CreateUserAsync(mappedUser, request.Password);
                if (!createResponse.Success)
                {
                    throw new BadRequestException($"could not create user \n{string.Join(",", createResponse.Errors!)}");
                }
                else
                {
                    response.Data = 1;
                    response.Success = true;
                    response.Message = "Created successfully!";
                }
            }
            return response;
        }
    }
}
