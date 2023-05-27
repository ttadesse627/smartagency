

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public record DeleteRequestedCommand(Guid id) : IRequest<ServiceResponse<Int32>> { }
public class DeleteRequestedCommandHandler : IRequestHandler<DeleteRequestedCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    public DeleteRequestedCommandHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<int>> Handle(DeleteRequestedCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var applicant = await _applicantRepository.GetAsync(request.id);
        if (applicant.IsRequested)
        {
            applicant.IsRequested = false;
            response = await _applicantRepository.SaveDbUpdateAsync();
        }
        if (response.Data > 0)
        {
            response.Message = "Deleted Successfully!";
            response.Success = true;
        }
        return response;
    }
}