using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Create
{
    public class CreateApplicantFollowupStatusCommandHandler : IRequestHandler<CreateApplicantFollowupStatusCommand, CreateApplicantFollowupStatusCommandResponse>
    {
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;
        private readonly IApplicantRepository _applicantRepository;
        public CreateApplicantFollowupStatusCommandHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository, IApplicantRepository applicantRepository)
        {
            _applicantFollowupStatusRepository = applicantFollowupStatusRepository;
            _applicantRepository = applicantRepository;
        }
        public async Task<CreateApplicantFollowupStatusCommandResponse> Handle(CreateApplicantFollowupStatusCommand request, CancellationToken cancellationToken)
        {
            // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createApplicantFollowupStatusCommandResponse = new CreateApplicantFollowupStatusCommandResponse();

            var validator = new CreateApplicantFollowupStatusCommandValidator(_applicantFollowupStatusRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createApplicantFollowupStatusCommandResponse.Success = false;
                createApplicantFollowupStatusCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createApplicantFollowupStatusCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createApplicantFollowupStatusCommandResponse.Message = createApplicantFollowupStatusCommandResponse.ValidationErrors[0];
            }
            if (createApplicantFollowupStatusCommandResponse.Success)
            {

                var serviceResponse = await _applicantRepository.GetApplicantByPassportNumber(request.applicantFollowupStatus.PassportNumber);
                var ApplicantId = serviceResponse.Data.Id;

                var applicantFollowupStatus = new ApplicantFollowupStatus()
                {
                    PassportNumber = request.applicantFollowupStatus.PassportNumber,
                    FollowupStatusId = request.applicantFollowupStatus.FollowupStatusId,
                    Remark = request.applicantFollowupStatus.Remark,
                    Month = request.applicantFollowupStatus.Month,
                    ApplicantId = ApplicantId

                };


            
                await _applicantFollowupStatusRepository.InsertAsync(applicantFollowupStatus, cancellationToken);
                var result = await _applicantFollowupStatusRepository.SaveChangesAsync(cancellationToken);
            }
            return createApplicantFollowupStatusCommandResponse;
        }
    }
}