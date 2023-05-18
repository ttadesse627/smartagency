using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Applicants.OnlineApplicants
{
    public class CreateOnlineApplicantCommandHandler : IRequestHandler<CreateOnlineApplicantCommand, CreateOnlineApplicantCommandResponse>
{
        private readonly IOnlineApplicantRepository _onlineApplicantRepository;
        public CreateOnlineApplicantCommandHandler(IOnlineApplicantRepository onlineApplicantRepository)
        {
            _onlineApplicantRepository = onlineApplicantRepository;
        }
        public async Task<CreateOnlineApplicantCommandResponse> Handle(CreateOnlineApplicantCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createOnlineApplicantCommandResponse = new CreateOnlineApplicantCommandResponse();

            var validator = new CreateOnlineApplicantCommandValidator(_onlineApplicantRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createOnlineApplicantCommandResponse.Success = false;
                createOnlineApplicantCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createOnlineApplicantCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createOnlineApplicantCommandResponse.Message = createOnlineApplicantCommandResponse.ValidationErrors[0];
            }
            if (createOnlineApplicantCommandResponse.Success)
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

                var onlineApplicantEntity = CustomMapper.Mapper.Map<OnlineApplicant>(request.onlineApplicant);
            await _onlineApplicantRepository.InsertAsync(onlineApplicantEntity, cancellationToken);
            await _onlineApplicantRepository.SaveChangesAsync(cancellationToken);      
            }
            return createOnlineApplicantCommandResponse;
        }
}
}