using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.OnlineApplicants.Query;
public class GetOnlineApplicantByIdQuery : IRequest<OnlineApplicantResponseDTO>
{
    public Guid Id { get; private set; }

    public GetOnlineApplicantByIdQuery(Guid Id)
    {
        this.Id = Id;
    }

}

public class GetOnlineApplicantByIdHandler : IRequestHandler<GetOnlineApplicantByIdQuery, OnlineApplicantResponseDTO>
{
    private readonly IOnlineApplicantRepository _onlineApplicantRepository;

    public GetOnlineApplicantByIdHandler(IOnlineApplicantRepository onlineApplicantQueryRepository)
    {
        _onlineApplicantRepository = onlineApplicantQueryRepository;
    }
    public async Task<OnlineApplicantResponseDTO> Handle(GetOnlineApplicantByIdQuery request, CancellationToken cancellationToken)
    {
        var selectedOnlineApplicant = await _onlineApplicantRepository.GetByIdAsync(request.Id);
        return CustomMapper.Mapper.Map<OnlineApplicantResponseDTO>(selectedOnlineApplicant);
    }
}