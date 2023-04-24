
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetAllLookUps: IRequest<List<LookUpResponseDTO>>
    {
    }
}