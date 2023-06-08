
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.DeletedInfos.Command.Update;
public record RestoreDeleteApplicantCommand(Guid Id) : IRequest<ServiceResponse<Int32>> { }

public class RestoreDeleteApplicantCommandHandler : IRequestHandler<RestoreDeleteApplicantCommand, ServiceResponse<Int32>>
{
    private readonly IApplicantRepository _applicantRepository;

    public RestoreDeleteApplicantCommandHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<Int32>> Handle(RestoreDeleteApplicantCommand command, CancellationToken cancellationToken)
    {
        var response = new ServiceResponse<int>();

        var applicantTobeRestored = await _applicantRepository.GetAsync(command.Id);
        if (applicantTobeRestored is not null)
        {
            applicantTobeRestored.IsDeleted = false;
            response.Success = await _applicantRepository.SaveChangesAsync(cancellationToken);
            if (response.Success)
            {
                response.Message = $"Successfully Restored the applicant with an id {command.Id}";
                response.Data = 1;
            }
        }
        else if (applicantTobeRestored is null)
        {
            response.Message = $"An applicant with an Id {command.Id} is not found!";
            response.Success = false;
        }
        else throw new Exception("Unknown error occorred while trying to restore the applicant.");
        return response;
    }
}