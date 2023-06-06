using System.Linq;
using System.Net.Cache;
using AppDiv.CRVS.Application.Exceptions;
using AppDiv.CRVS.Application.Interfaces;
using AppDiv.CRVS.Application.Interfaces.Persistence;
using AppDiv.CRVS.Application.Mapper;
using AppDiv.CRVS.Domain;
using AppDiv.CRVS.Domain.Entities;
using AppDiv.CRVS.Domain.Repositories;
using AppDiv.CRVS.Utility.Config;
using AppDiv.CRVS.Utility.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace AppDiv.CRVS.Application.Features.User.Command.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IGroupRepository _groupRepository;
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IOptions<SMTPServerConfiguration> config;
        private readonly SMTPServerConfiguration _config;

        public CreateUserCommandHandler(IIdentityService identityService,
                                        IGroupRepository groupRepository,
                                        IFileService fileService, IMailService mailService,
                                        ISmsService smsService,
                                        IOptions<SMTPServerConfiguration> config)
        {
            this._groupRepository = groupRepository;
            _identityService = identityService;
            _fileService = fileService;
            _mailService = mailService;
            _smsService = smsService;
            this.config = config;
            _config = config.Value;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {


            var CreateUserCommadResponse = new CreateUserCommandResponse();

            var validator = new CreateUserCommandValidator(_identityService);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                CreateUserCommadResponse.Success = false;
                CreateUserCommadResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    CreateUserCommadResponse.ValidationErrors.Add(error.ErrorMessage);
                CreateUserCommadResponse.Message = CreateUserCommadResponse.ValidationErrors[0];
            }
            if (CreateUserCommadResponse.Success)
            {
           
                var listGroup = await _groupRepository.GetMultipleUserGroups(request.UserGroups);

              
                var user =  CustomMapper.Mapper.Map<ApplicationUser>(request);
                user.PhoneNumber = user.PersonalInfo.ContactInfo.Phone;
                user.UserGroups = listGroup;

                var response = await _identityService.createUser(user);
                if (!response.result.Succeeded)
                {
                    throw new BadRequestException($"could not create user \n{string.Join(",",response.result.Errors)}");
                }

                // save profile image
                var file = request.UserImage;
                var folderName = Path.Combine("Resources", "UserProfiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = response.id;
                if(!string.IsNullOrEmpty(file)){

                await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);
                }

                //send password by email    
                var content = response.password + "  is your default password you can login and change it";
                var subject = "Welcome to OCRVS";
                await _mailService.SendAsync(body: content, subject: subject, senderMailAddress: _config.SENDER_ADDRESS, receiver: user.Email, cancellationToken);

                //send password by phone 
                await _smsService.SendSMS(user.PhoneNumber , subject +"\n"+content);
            
            
            }
            return CreateUserCommadResponse;
        }
    }
}
