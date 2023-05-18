using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Command.Create
{
    public class CreatePageCommandHandler: IRequestHandler<CreatePageCommand, CreatePageCommandResponse>
{
        private readonly IPageRepository _pageRepository;
        public CreatePageCommandHandler(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public async Task<CreatePageCommandResponse> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createPageCommandResponse = new CreatePageCommandResponse();

            var validator = new CreatePageCommandValidator(_pageRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createPageCommandResponse.Success = false;
                createPageCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createPageCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createPageCommandResponse.Message = createPageCommandResponse.ValidationErrors[0];
            }
            if (createPageCommandResponse.Success)
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

                var pageEntity = CustomMapper.Mapper.Map<Page>(request.page);
            await _pageRepository.InsertAsync(pageEntity, cancellationToken);
            var result = await _pageRepository.SaveChangesAsync(cancellationToken);      
            }
            return createPageCommandResponse;
        }
}
}