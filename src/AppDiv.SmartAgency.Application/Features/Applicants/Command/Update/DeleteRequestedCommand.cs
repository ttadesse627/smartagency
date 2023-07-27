

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public record DeleteRequestedCommand(Guid id) : IRequest<ServiceResponse<Int32>> { }
public class DeleteRequestedCommandHandler : IRequestHandler<DeleteRequestedCommand, ServiceResponse<Int32>>
{
    private readonly IRequestedApplicantRepository _requestedApplicantRepository;
    public DeleteRequestedCommandHandler(IRequestedApplicantRepository applicantRepository)
    {
        _requestedApplicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<int>> Handle(DeleteRequestedCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<Int32>();
        var applicant = await _requestedApplicantRepository.GetAsync(request.id);
        if (applicant != null)
        {
            _requestedApplicantRepository.Delete(applicant);
            try
            {
                bool success = await _requestedApplicantRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception)
            {

                throw;
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