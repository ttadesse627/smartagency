

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
public class DeleteApplicantCommand : IRequest<ServiceResponse<Int32>>
{
    public bool IsDeleted { get; set; }
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
        var applEntity = await _applicantRepository.GetApplicantAsync(request.Id);
        if (applEntity is not null)
        {
            applEntity.IsDeleted = request.IsDeleted;
            serviceResponse = await _applicantRepository.EditApplicantAsync();
            if (serviceResponse.Data >= 1)
            {
                serviceResponse.Message = $"Successfully deleted the applicant with an id {request.Id}";
                serviceResponse.Success = true;
            }
        }
        else if (applEntity is null)
        {
            serviceResponse.Message = $"An applicant with an Id {request.Id} is not found!";
            serviceResponse.Success = false;
        }
        return serviceResponse;
    }
}
