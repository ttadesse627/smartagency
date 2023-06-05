using System.Text.RegularExpressions;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppDiv.SmartAgency.Application.Features.User.Command.Update;
public record UpdateUserCommand(UpdateUserRequest request) : IRequest<ServiceResponse<Int32>> { }

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResponse<Int32>>
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
    public async Task<ServiceResponse<Int32>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var response = new ServiceResponse<Int32>();
        ICollection<UserGroup> listGroup = new List<UserGroup>();
        if (request.UserGroups.Count > 0)
        {
            listGroup = await _groupRepository.GetMultipleUserGroups(request.UserGroups);
        }
        var explLoadedProps = new string[] { "Partner", "Address", "Address.AddressRegion", "Address.Country", "UserGroups" };
        var user = await _userRepository.GetWithPredicateAsync(appl => appl.Id == request.Id.ToString(), explLoadedProps);

        if (user != null)
        {
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PositionId = request.PositionId;
            user.BranchId = request.BranchId;
            user.PartnerId = request.PartnerId;
            user.Address = new Address
            {
                CountryId = request.Address?.CountryId,
                AddressRegionId = request.Address?.AddressRegionId,
                Zone = request.Address?.Zone,
                Woreda = request.Address?.Woreda,
                Kebele = request.Address?.Kebele,
                PhoneNumber = request.Address?.PhoneNumber,
                Email = request.Address?.Email,
                Website = request.Address?.Website
            };
            user.UserGroups = listGroup;
        }

        try
        {
            response.Success = await _userRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            response.Errors?.Add(ex.Message);
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
