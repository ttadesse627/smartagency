using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;

using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Enjazs.Command.Create;
public record CreateEnjazCommand(CreateEnjazRequest Request) : IRequest<ServiceResponse<Int32>>
{ }
public class CreateEnjazCommandHandler : IRequestHandler<CreateEnjazCommand, ServiceResponse<Int32>>
{
    private readonly IEnjazRepository _enjazRepository;
    public CreateEnjazCommandHandler(IEnjazRepository enjazRepository)
    {
        _enjazRepository = enjazRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(CreateEnjazCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var enjazEntities = new List<Enjaz>();

        if (command.Request.Enjazs != null && command.Request.Enjazs.Any())
        {
            foreach (var enjaz in command.Request.Enjazs)
            {
                var enjazEntity = CustomMapper.Mapper.Map<Enjaz>(enjaz);
                enjazEntities.Add(enjazEntity);
            }
        }

        await _enjazRepository.InsertAsync(enjazEntities, cancellationToken);
        response.Success = await _enjazRepository.SaveChangesAsync(cancellationToken);
        if (response.Success)
        {
            response.Data = enjazEntities.GetHashCode();
            response.Message = $"Operation Succeeded: {response.Data} entity is created!";
        }

        return response;
    }
}