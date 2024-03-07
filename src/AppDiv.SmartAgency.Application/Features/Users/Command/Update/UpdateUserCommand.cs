using System.Text.RegularExpressions;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppDiv.SmartAgency.Application.Features.User.Command.Update;
public record UpdateUserCommand(UpdateUserRequest request) : IRequest<ServiceResponse<int>> { }

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResponse<int>>
{
    private readonly IIdentityService _identityService;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IIdentityService identityService, IGroupRepository groupRepository, ILogger<UpdateUserCommandHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _groupRepository = groupRepository;
        _identityService = identityService;
        _userRepository = userRepository;
    }
    public async Task<ServiceResponse<int>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        ServiceResponse<int> response = new ServiceResponse<int>();
        List<UserGroup> listGroup = [];
        if (request.UserGroups?.Count > 0)
        {
            listGroup = await _groupRepository.GetMultipleUserGroups(request.UserGroups);
        }
        // var explLoadedProps = new string[] { "Partner", "Address", "Address.Region", "UserGroups" };
        // var user = await _userRepository.GetWithPredicateAsync(appl => appl.Id == request.Id.ToString(), explLoadedProps);
        var user = CustomMapper.Mapper.Map<ApplicationUser>(request);
        if (user != null)
        {
            user.Email = request.Address!.Email;
            user.UserGroups = listGroup;
            await _userRepository.UpdateAsync(user, user => user.Id);
            try
            {
                response.Success = await _userRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Message = "The user is successfully Updated!";
                    response.Data = 1;
                }

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
        }
        return response;
    }
    private bool isValidBase64String(string? base64String)
    {
        if (base64String == null)
        {
            return true;
        }
        try
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64String = regex.Replace(base64String, string.Empty);

            Convert.FromBase64String(base64String);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
