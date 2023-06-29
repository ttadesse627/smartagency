
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Users.Command.Delete;
public record DeleteEnjazCommand(Guid Id) : IRequest<ServiceResponse<int>> { }

public class DeleteEnjazCommandHandler : IRequestHandler<DeleteEnjazCommand, ServiceResponse<int>>
{
    private readonly IEnjazRepository _enjazRepository;
    public DeleteEnjazCommandHandler(IEnjazRepository enjazRepository)
    {
        _enjazRepository = enjazRepository;
    }

    public async Task<ServiceResponse<int>> Handle(DeleteEnjazCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<int>();
        var enjazToDelete = _enjazRepository.GetAsync(request.Id);
        try
        {
            _enjazRepository.Delete(enj => enj.Id == request.Id);
            response.Success = await _enjazRepository.SaveChangesAsync(cancellationToken);
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