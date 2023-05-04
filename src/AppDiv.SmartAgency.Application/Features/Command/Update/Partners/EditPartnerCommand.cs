
using AppDiv.SmartAgency.Application.Contracts.DTOs.AddressDTOs;
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
        public EditPartnerCommandHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }
        public async Task<PartnerResponseDTO> Handle(EditPartnerCommand request,  CancellationToken cancellationToken)
        {
             var partnerEntity = CustomMapper.Mapper.Map<Partner>(request);
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
                Address = request.Address

            };
*/
            try
            {
                await _partnerRepository.UpdateAsync(partnerEntity, x => x.Id);
                 await _partnerRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
            var partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

            return partnerResponse;
        }
    }

}