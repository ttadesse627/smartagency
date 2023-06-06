

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public record RequestApplicantCommand(Guid id) : IRequest<ServiceResponse<Int32>> { }
public class RequestApplicantCommandHandler : IRequestHandler<RequestApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    public RequestApplicantCommandHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<int>> Handle(RequestApplicantCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var applicant = await _applicantRepository.GetAsync(request.id);
        if (applicant != null)
        {
            if (!applicant.IsRequested)
            {
                applicant.IsRequested = true;
                response.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
            }
            if (response.Success)
            {
                response.Message = "Request Successful!";
            }
        }
        else
        {
            throw new NotFoundException($"The applicant with Id {request.id} is not found!");
        }
        return response;
    }
}