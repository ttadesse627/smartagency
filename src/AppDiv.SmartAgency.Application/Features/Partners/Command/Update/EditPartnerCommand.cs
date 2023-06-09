
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Base;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Update.Partners
{
    public class EditPartnerCommand : IRequest<PartnerResponseDTO>
    {
        public Guid Id { get; set; }
        public string PartnerType { get; set; }
        public string PartnerName { get; set; }
        public string PartnerNameAmharic { get; set; }
        public string PartnerNameArabic { get; set; }
        public string ContactPerson { get; set; }
        public string IdNumber { get; set; }
        public string ManagerNameAmharic { get; set; }
        public string LicenseNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string HeaderLogo { get; set; }
        public string ReferenceNumber { get; set; }
        public Guid AddressId { get; set; }

        public AddressResponseDTO Address { get; set; }

       
    }



   

    public class EditPartnerCommandHandler : IRequestHandler<EditPartnerCommand, PartnerResponseDTO>
    {
   
        private readonly IPartnerRepository _partnerRepository;
        private readonly IPartnerRepository _partnerQueryRepository;
        public EditPartnerCommandHandler(IPartnerRepository partnerRepository,IPartnerRepository partnerQueryRepository)
        {
            _partnerRepository = partnerRepository;
            _partnerQueryRepository=partnerQueryRepository;
        }
        public async Task<PartnerResponseDTO> Handle(EditPartnerCommand request,  CancellationToken cancellationToken)
        {
            var partnerResponse = new PartnerResponseDTO();
          //  Partner partnerEntity = new Partner
          /*  {
                Id = request.Id,
                PartnerName = request.PartnerName,
                PartnerType = request.PartnerType,
                PartnerNameAmharic = request.PartnerNameAmharic,
                PartnerNameArabic = request.PartnerNameArabic,
                ContactPerson = request.ContactPerson,
                IdNumber = request.IdNumber,
                ManagerNameAmharic = request.ManagerNameAmharic,
                LicenseNumber = request.LicenseNumber,
                BankName = request.BankName,
                BankAccount = request.BankAccount,
                HeaderLogo = request.HeaderLogo,
                ReferenceNumber = request.ReferenceNumber,
                PartnerAddress = request.Address

            };
            
*/
            var partnerEntity = CustomMapper.Mapper.Map<Partner>(request);
            try
            {
                var res =    await _partnerRepository.UpdateAsync(partnerEntity);
                // await _partnerRepository.SaveChangesAsync(cancellationToken);

                if(res>=1) {

                    var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
                    Console.WriteLine(modifiedPartner);
                    partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);
                    Console.WriteLine(partnerResponse);

                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
           
     
          //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
           // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

            return partnerResponse;
        }
    }

}