using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Delete;
public record DeleteUserCommand(Guid Id) : IRequest<ServiceResponse<int>> { }

public class DeleteLookupCommandHandler : IRequestHandler<DeleteUserCommand, ServiceResponse<int>>
{
    private readonly IIdentityService _identityService;
    public DeleteLookupCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ServiceResponse<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<int>();
        try
        {
            response.Success = await _identityService.DeleteUserAsync(request.Id.ToString());
            if (response.Success)
            {
                response.Message = "Deleted Successfully!";
            }
        }
        catch (Exception exp)
        {
            throw (new ApplicationException(exp.Message));
        }

        return response;
    }
}