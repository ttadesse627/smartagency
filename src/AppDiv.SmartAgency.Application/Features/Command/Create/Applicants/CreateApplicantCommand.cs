

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
            //can use this instead of automapper

            // var applicantEntity = new Applicant
            // {
            //     FirstName = request.FirstName,
            //     MiddleName = request.MiddleName,
            //     LastName = request.LastName,
            //     BirthDate = request.BirthDate,
            //     PassportNumber = request.PassportNumber,
            //     IssuingCountry = request.IssuingCountry,
            //     IssuedDate = request.IssuedDate,
            //     IssuedPlace = request.IssuedPlace,
            //     PassportExpiryDate = request.PassportExpiryDate,
            //     PlaceOfBirth = request.PlaceOfBirth,
            //     AmharicFullName = request.AmharicFullName,
            //     ArabicFullName = request.ArabicFullName,
            //     MaritalStatus = request.MaritalStatus,
            //     Complexion = request.Complexion,
            //     NumberOfChildren = request.NumberOfChildren,
            //     Health = request.Health,
            //     ReligionId = request.ReligionId,
            //     LookUpJobTitles = request.JobTitles,
            //     Salary = request.Salary,
            //     DesiredCountry = request.DesiredCountry,
            //     MotherFullName = request.MotherFullName,
            //     PreviousCountry = request.PreviousCountry,
            //     CurrentNationality = request.CurrentNationality,
            //     Height = request.Height,
            //     ContractPeriod = request.ContractPeriod,
            //     JobTitleAmharic = request.JobTitleAmharic,
            //     BrokerName = request.BrokerName,
            //     Branch = request.Branch,
            //     Remark = request.Remark,
            //     PartnerId = request.PartnerId,
            //     Languages = request.Languages,
            // };

            var applicantEntity = CustomMapper.Mapper.Map<Applicant>(applicantRequest.applicantRequest);
            await _applicantRepository.InsertAsync(applicantEntity, cancellationToken);
            var result = await _applicantRepository.SaveChangesAsync(cancellationToken);
        }

        return createApplicantResponse;
    }
}