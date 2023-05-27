

using AppDiv.SmartAgency.Application.Common;
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
        if (!applicant.IsRequested)
        {
            applicant.IsRequested = true;
            response = await _applicantRepository.SaveDbUpdateAsync();
        }
        if (response.Data > 0)
        {
            response.Message = "Request Successful!";
            response.Success = true;
        }
        return response;
    }
}