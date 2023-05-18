using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Partners.Command.Create
{
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, CreatePartnerCommandResponse>
{
        private readonly IPartnerRepository _partnerRepository;
        public CreatePartnerCommandHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }
        public async Task<CreatePartnerCommandResponse> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createPartnerCommandResponse = new CreatePartnerCommandResponse();

            var validator = new CreatePartnerCommandValidator(_partnerRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createPartnerCommandResponse.Success = false;
                createPartnerCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createPartnerCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createPartnerCommandResponse.Message = createPartnerCommandResponse.ValidationErrors[0];
            }
            if (createPartnerCommandResponse.Success)
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

                var partnerEntity = CustomMapper.Mapper.Map<Partner>(request.partner);
            await _partnerRepository.InsertAsync(partnerEntity, cancellationToken);
            var result = await _partnerRepository.SaveChangesAsync(cancellationToken);      
            }
            return createPartnerCommandResponse;
        }
}
}