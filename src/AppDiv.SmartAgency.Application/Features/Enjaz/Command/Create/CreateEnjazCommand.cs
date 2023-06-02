using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;

using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Enjazs.Command.Create;
public record CreateEnjazCommand(AddEnjazRequest request) : IRequest<ServiceResponse<Int32>>
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
        var createEnjazCommandResponse = new ServiceResponse<Int32>();
        
        var enjazEntity = CustomMapper.Mapper.Map<Enjaz>(command.request);

        await _enjazRepository.InsertAsync(enjazEntity, cancellationToken);
        createEnjazCommandResponse.Success = await _enjazRepository.SaveChangesAsync(cancellationToken);
        if (createEnjazCommandResponse.Success)
        {
            createEnjazCommandResponse.Data = enjazEntity.GetHashCode();
            createEnjazCommandResponse.Message = $"Operation Succeeded: {createEnjazCommandResponse.Data} entity is created!";
        }         

        return createEnjazCommandResponse;
    }
}