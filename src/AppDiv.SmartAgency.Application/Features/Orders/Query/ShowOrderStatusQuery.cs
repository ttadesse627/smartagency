using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Orders.Query;
public record ShowOrderStatusQuery(Guid ApplicantId) : IRequest<ShowOrderStatusResponseDTO> { }

public class ShowOrderStatusQueryHandler : IRequestHandler<ShowOrderStatusQuery, ShowOrderStatusResponseDTO>
{
    private readonly IApplicantRepository _applicantRepository;
    public ShowOrderStatusQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<ShowOrderStatusResponseDTO> Handle(ShowOrderStatusQuery request, CancellationToken cancellationToken)
    {
        var res = await _applicantRepository.GetShowOrderStatus(request.ApplicantId);
        return res;
    }
}