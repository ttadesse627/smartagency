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
    public class EditApplicantFollowupStatusCommand: IRequest<string>
    {
        public Guid Id {get; set;}
        public string PassportNumber {get; set;} 
        public DateTime Month {get; set;}  
        public string Remark {get; set;} 
        
        public Guid FollowupStatusId {get; set;}

       
    }



   

    public class EditApplicantFollowupStatusCommandHandler : IRequestHandler<EditApplicantFollowupStatusCommand, string>
    {
          private readonly IApplicantRepository _applicantRepository;
        private readonly IApplicantFollowupStatusRepository _applicantFollowupStatusRepository;
        public EditApplicantFollowupStatusCommandHandler(IApplicantFollowupStatusRepository applicantFollowupStatusRepository, IApplicantRepository applicantRepository)
        {
            _applicantFollowupStatusRepository = applicantFollowupStatusRepository;
            _applicantRepository = applicantRepository;
            
        }
        public async Task<string> Handle(EditApplicantFollowupStatusCommand request,  CancellationToken cancellationToken)
        {
           int response = 0;
    
            try
            {
                
                var serviceResponse= await _applicantRepository.GetApplicantByPassportNumber(request.PassportNumber);
                if (serviceResponse.Data!= null)
                {
                   var followupStatus = new ApplicantFollowupStatus()
                {
                     Id=request.Id,
                     PassportNumber=request.PassportNumber,
                     FollowupStatusId=request.FollowupStatusId,
                     Month= request.Month,
                     Remark= request.Remark,
                     ApplicantId= serviceResponse.Data.Id
                     
                };
                var res =    await _applicantFollowupStatusRepository.UpdateAsync(followupStatus);

                if(res>=1) {

                  response= res;
                }
                }else{
                    return "No data found with "+ request.PassportNumber+ " passport number";
                }
                
               
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
         return response + " record updated";

        }
    }
}