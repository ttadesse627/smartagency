using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Deposits
{
    public class CreateDepositCommandHandler: IRequestHandler<CreateDepositCommand, CreateDepositCommandResponse>
{
        private readonly IDepositRepository _depositRepository;
         private readonly IApplicantRepository _applicantRepository;
        public CreateDepositCommandHandler(IDepositRepository depositRepository, IApplicantRepository applicantRepository)
        {
            _depositRepository = depositRepository;
            _applicantRepository= applicantRepository;
        }
        public async Task<CreateDepositCommandResponse> Handle(CreateDepositCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createDepositCommandResponse = new CreateDepositCommandResponse();

            var validator = new CreateDepositCommandValidator(_depositRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createDepositCommandResponse.Success = false;
                createDepositCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createDepositCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createDepositCommandResponse.Message = createDepositCommandResponse.ValidationErrors[0];
            }
            if (createDepositCommandResponse.Success)
            {
                //can use this instead of automapper
              /*  var partner = new Partner()
                {
                    PartnerName=request.partner.PartnerName,
                    PartnerType=request.partner.PartnerType,
                    PartnerNameAmharic=request.partner.PartnerNameAmharic,
                    PartnerNameArabic=request.partner.PartnerNameArabic,
                    ContactPerson=request.partner.ContactPerson,
                    IdNumber=request.partner.IdNumber,
                    ManagerNameAmharic=request.partner.ManagerNameAmharic,
                    LicenseNumber=request.partner.LicenseNumber,
                    BankName=request.partner.BankName,
                    BankAccount=request.partner.BankAccount,
                    HeaderLogo=request.partner.HeaderLogo,
                    ReferenceNumber=request.partner.ReferenceNumber,
                    Address=request.partner.Address
                };  */

                
               // await _partnerRepository.InsertAsync(partner, cancellationToken);
                //await _partnerRepository.SaveChangesAsync(cancellationToken);


            var serviceResponse= await _applicantRepository.GetApplicantByPassportNumber(request.deposit.PassportNumber);
            var ApplicantId=serviceResponse.Data.Id;

    var deposit = new Deposit()
                {
                  PassportNumber=request.deposit.PassportNumber,
                  DepositAmount=request.deposit.DepositAmount,
                  DepositedBy= request.deposit.DepositedBy,
                  Month= request.deposit.Month,
                  ApplicantId=ApplicantId 
                   
                };  


          //  var depositEntity = CustomMapper.Mapper.Map<Deposit>(request.deposit);
            await _depositRepository.InsertAsync(deposit, cancellationToken);
            var result = await _depositRepository.SaveChangesAsync(cancellationToken);      
            }
            return createDepositCommandResponse;
        }
}
}