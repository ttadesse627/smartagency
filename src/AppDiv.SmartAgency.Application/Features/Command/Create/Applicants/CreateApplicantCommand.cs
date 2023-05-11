

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Applicants;
public record CreateApplicantCommand(CreateApplicantRequest applicantRequest) : IRequest<ServiceResponse<CreateApplicantResponseDTO>>
{

}

public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, ServiceResponse<CreateApplicantResponseDTO>>
{
    private readonly IApplicantRepository _applicantRepository;
    public CreateApplicantCommandHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    public async Task<ServiceResponse<CreateApplicantResponseDTO>> Handle(CreateApplicantCommand applicantRequest, CancellationToken cancellationToken)
    {
        var createApplicantResponse = new ServiceResponse<CreateApplicantResponseDTO>();
        var request = applicantRequest.applicantRequest;
        var createApplicantCommandValidator = new CreateApplicantCommandValidator(_applicantRepository);
        var validationResult = await createApplicantCommandValidator.ValidateAsync(applicantRequest, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            createApplicantResponse.Success = false;
            createApplicantResponse.Errors = new List<string>();
            foreach (var error in validationResult.Errors)
                createApplicantResponse.Errors.Add(error.ErrorMessage);
            createApplicantResponse.Message = createApplicantResponse.Errors[0];
        }
        if (createApplicantResponse.Success)
        {

            var applicantEntity = CustomMapper.Mapper.Map<Applicant>(applicantRequest.applicantRequest);
            var success = await _applicantRepository.CreateApplicantAsync(applicantEntity);
            if(success >= 1)
            {
                createApplicantResponse.Message = "The applicant is successfully added.";
                createApplicantResponse.Success = true;
            }
            else
            {
                createApplicantResponse.Message = "Couldn't add the requested applicant.";
                createApplicantResponse.Success = false;
            }
        }

        return createApplicantResponse;
    }
}