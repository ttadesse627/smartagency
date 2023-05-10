using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Update.Deposits
{
    public class EditDepositCommand: IRequest<GetDepositByIdResponseDTO>
    {
        public Guid Id {get; set;}
        public string PassportNumber {get; set;} 
        public double DepositAmount {get; set;} 
        public DateTime Month {get; set;}  
        public string DepositedBy {get; set;} 
        public Guid ApplicantId {get; set;}

       
    }



   

    public class EditDepositCommandHandler : IRequestHandler<EditDepositCommand, GetDepositByIdResponseDTO>
    {
   
        private readonly IDepositRepository _depositRepository;
        private readonly IDepositRepository _depositQueryRepository;
        public EditDepositCommandHandler(IDepositRepository depositRepository,IDepositRepository depositQueryRepository)
        {
            _depositRepository = depositRepository;
            _depositQueryRepository=depositQueryRepository;
        }
        public async Task<GetDepositByIdResponseDTO> Handle(EditDepositCommand request,  CancellationToken cancellationToken)
        {
            var depositResponse = new GetDepositByIdResponseDTO();
          
            var depositEntity = CustomMapper.Mapper.Map<Deposit>(request);
            try
            {
                var res =    await _depositRepository.UpdateAsync(depositEntity);
                // await _partnerRepository.SaveChangesAsync(cancellationToken);

                if(res>=1) {

                    var modifiedDeposit = await _depositQueryRepository.GetByIdAsync(request.Id);
                    depositResponse = CustomMapper.Mapper.Map<GetDepositByIdResponseDTO>(modifiedDeposit);
                    Console.WriteLine(depositResponse);

                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
           
     
          //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
           // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

            return depositResponse;
        }
    }
}