

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public class DeleteApplicantCommand : IRequest<ServiceResponse<Int32>>
{
    public Guid Id { get; set; }
    public DeleteApplicantCommand(Guid id)
    {
        Id = id;
    }
}
public class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;
    public DeleteApplicantCommandHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
    {
        var serviceResponse = new ServiceResponse<Int32>();
        var deletedApplicant = await _applicantRepository.GetApplicantByIdAsync(request.Id);
        if (deletedApplicant is not null)
        {
            deletedApplicant.IsDeleted = true;
            serviceResponse.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
            if (serviceResponse.Success == true)
            {
                serviceResponse.Message = $"Successfully deleted the applicant with an id {request.Id}";
            }
        }
        else if (deletedApplicant is null)
        {
            serviceResponse.Message = $"An applicant with an Id {request.Id} is not found!";
            throw new NotFoundException($"An applicant with an Id {request.Id} is not found!");
        }
        return serviceResponse;
    }
}
