
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Partners.Command.Update
{
    public class EditPartnerCommand : IRequest<string>
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
        public string? HeaderLogo { get; set; }
        public string ReferenceNumber { get; set; }
         public Guid AddressId { get; set; }

        public EditPartnerAddressRequest Address { get; set; }
       
    }



   

    public class EditPartnerCommandHandler : IRequestHandler<EditPartnerCommand, string>
    {
        private readonly IFileService _fileService;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IPartnerRepository _partnerQueryRepository;
        public EditPartnerCommandHandler(IPartnerRepository partnerRepository,IPartnerRepository partnerQueryRepository, IFileService fileService)
        {
            _partnerRepository = partnerRepository;
            _partnerQueryRepository=partnerQueryRepository;
            _fileService = fileService;
        }
        public async Task<string> Handle(EditPartnerCommand request,  CancellationToken cancellationToken)
        {
           
            var partnerEntity = CustomMapper.Mapper.Map<Partner>(request);
            try
            {
                var res =    await _partnerRepository.UpdateAsync(partnerEntity);
                // await _partnerRepository.SaveChangesAsync(cancellationToken);

                if(res>=1) {

                    // save headerlogo
                var file = request.HeaderLogo;
                var folderName = Path.Combine("Resources", "PartnersHeaderLogo");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = request.Id.ToString();
                if(!string.IsNullOrEmpty(file)){

                 await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);
             } 


                   // var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
                    //partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);
             

                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
           
     
          //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
           // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

            return "sucessfully updated";
        }
    }

}