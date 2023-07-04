using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Create
{
    public record CreateUserCommand(AddUserRequest request) : IRequest<ServiceResponse<int>> { }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceResponse<int>>
    {
        private readonly IIdentityService _identityService;
        private readonly IGroupRepository _groupRepository;
        public CreateUserCommandHandler(IIdentityService identityService, IGroupRepository groupRepository)
        {
            _identityService = identityService;
            _groupRepository = groupRepository;
        }
        public async Task<ServiceResponse<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            var request = command.request;
            if (request.Password != request.ConfirmationPassword)
            {
                var msg = "The password should be confirmed";
                response.Errors.Add(msg);
                throw new BadRequestException("The password should be confirmed");
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
                mappedUser.Email = request.Address.Email;
                if (request.UserGroups.Count > 0)
                {
                    var listGroup = await _groupRepository.GetMultipleUserGroups(request.UserGroups);
                    mappedUser.UserGroups = listGroup;
                }

                var createResponse = await _identityService.CreateUserAsync(mappedUser, request.Password);
                if (!createResponse.Success)
                {
                    throw new BadRequestException($"could not create user \n{string.Join(",", createResponse.Errors)}");
                }
                else
                {
                    response.Data = 1;
                    response.Success = true;
                    response.Message = "Created successfully!";
                }

                // save profile image
                // var file = request.UserImage;
                // var folderName = Path.Combine("Resources", "UserProfiles");
                // var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                // var fileName = response.id;
                // if (!string.IsNullOrEmpty(file))
                // {

                //     await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);
                // }

                // //send password by email    
                // var content = response.password + "  is your default password you can login and change it";
                // var subject = "Welcome to OCRVS";
                // await _mailService.SendAsync(body: content, subject: subject, senderMailAddress: _config.SENDER_ADDRESS, receiver: user.Email, cancellationToken);

                // //send password by phone 
                // await _smsService.SendSMS(user.PhoneNumber, subject + "\n" + content);


            }
            return response;
        }
    }
}
