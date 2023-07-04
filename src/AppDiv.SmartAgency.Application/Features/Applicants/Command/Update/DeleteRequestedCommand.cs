

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
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
        if (applicant != null)
        {
            if (applicant.IsRequested)
            {
                applicant.IsRequested = false;
                response.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
            }
            if (response.Success)
            {
                response.Message = "Deleted Successfully!";
            }
        }
        else
        {
            response.Success = false;
            throw new NotFoundException($"The applicant with Id {request.id} is not found!");
        }

        return response;
    }
}