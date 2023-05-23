using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Update
{
    public class EditApplicantFollowupStatusCommand: IRequest<GetApplicantFollowupStatusByIdResponseDTO>
    {
        public Guid Id {get; set;}
        public string PassportNumber {get; set;} 
        public DateTime Month {get; set;}  
        public string Remark {get; set;} 
        public Guid ApplicantId {get; set;}
        public Guid FollowupStatusId {get; set;}

       
    }



   

    public class EditApplicantFollowupStatusCommandHandler : IRequestHandler<EditApplicantFollowupStatusCommand, GetApplicantFollowupStatusByIdResponseDTO>
    {
        
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;
        public EditApplicantFollowupStatusCommandHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository)
        {
            _applicantFollowupStatusRepository = applicantFollowupStatusRepository;
            
        }
        public async Task<GetApplicantFollowupStatusByIdResponseDTO> Handle(EditApplicantFollowupStatusCommand request,  CancellationToken cancellationToken)
        {
            var applicantFollowupStatusResponse = new GetApplicantFollowupStatusByIdResponseDTO();
          
            var applicantFollowupStatusEntity = CustomMapper.Mapper.Map<ApplicantFollowupStatus>(request);
            try
            {
                var res =    await _applicantFollowupStatusRepository.UpdateAsync(applicantFollowupStatusEntity);

                if(res>=1) {

                    var modifiedApplicantFollowupStatus = await _applicantFollowupStatusRepository.GetByIdAsync(request.Id);
                    applicantFollowupStatusResponse = CustomMapper.Mapper.Map<GetApplicantFollowupStatusByIdResponseDTO>(modifiedApplicantFollowupStatus);
                 
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
        

            return applicantFollowupStatusResponse;
        }
    }
}